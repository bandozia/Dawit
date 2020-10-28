using Dawit.API.Model.Form;
using Dawit.API.Service.Neural;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Service.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dawit.API.Controllers
{
    [ApiController]
    [Route("neural")]
    public class NeuralController : ControllerBase
    {
        private readonly NeuralJobService _neuralJobService;
        private readonly INeuralJobSubscriber _nnSubscriber;

        public NeuralController(NeuralJobService neuralJobService, INeuralJobSubscriber nnSubscriber)
        {
            _neuralJobService = neuralJobService;
            _nnSubscriber = nnSubscriber;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(NeuralJobForm form)
        {
            var job = await _neuralJobService.CreateNeuralJob(form);
            return Created(job.Id.ToString(), job);
        }

        [HttpPost("train")]
        public async Task<IActionResult> StartTrain(TrainRequestForm trainRequest)
        {
            //TODO: check if user realy owns the job
            if (await _neuralJobService.TrainNeuralJob(Guid.Parse(trainRequest.JobId)))
                return Ok();
            else 
                return NotFound();
        }

        [HttpGet("subscribe/{jobId}")]
        public async Task Subscribe(CancellationToken ct, string jobId)
        {
            //TODO: check job exists
            //TODO: check client owns job or job is public
            Response.Headers.Add("Content-Type", "text/event-stream");
            bool jobComplete = false;

            Action<NeuralMetric> onProgress = async m =>
            {
                await Response.WriteAsync($"dados vindo{m.JobId}\n\n", ct);
                await Response.Body.FlushAsync();
            };

            Action<JobResult> onComplete = async r =>
            {
                await Response.WriteAsync($"acabou{r.JobId} \n\n", ct);
                await Response.Body.FlushAsync();
                jobComplete = true;
            };

            _nnSubscriber.Subscribe(Guid.Parse(jobId), onComplete, onProgress);

            while (!ct.IsCancellationRequested && !jobComplete)
            {
                await Task.Delay(1000);
            }

            _nnSubscriber.Unsubscribe(Guid.Parse(jobId), onProgress);
            _nnSubscriber.Unsubscribe(Guid.Parse(jobId), onComplete);
        }

    }
}
