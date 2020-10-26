using Dawit.API.Model.Form;
using Dawit.API.Service.Neural;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Service.Messaging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dawit.API.Controllers
{
    [ApiController]
    [Route("neural")]
    public class NeuralController : ControllerBase
    {
        private readonly NeuralJobService _neuralJobService;

        public NeuralController(NeuralJobService neuralJobService)
        {
            _neuralJobService = neuralJobService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(NeuralJobForm form)
        {            
            var job = await _neuralJobService.CreateNeuralJob(form);
            //TODO: insert uri
            return Created(job.Id.ToString(), job);
        }

        [HttpPost("train")]
        public async Task<IActionResult> StartTrain(string jobId)
        {
            //TODO: check if user realy owns the job
            await _neuralJobService.TrainNeuralJob(Guid.Parse(jobId));
            return Ok();
        }
    }
}
