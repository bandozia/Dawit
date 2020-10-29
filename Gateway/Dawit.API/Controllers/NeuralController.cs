using Dawit.API.Service.Neural;
using Dawit.Domain.Model.Neural;
using Dawit.Infrastructure.Service.Messaging;
using Dawit.Infrastructure.Service.Neural;
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
        private readonly INeuralNetworkService _neuralNetService;
        
        public NeuralController(INeuralNetworkService neuralnetService)
        {
            _neuralNetService = neuralnetService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(NeuralNetwork network)
        {
            var result = await _neuralNetService.CreateNeuralNetwork(network);
            return Created(network.Id.ToString(), network);
        }

        [HttpPost("train")]
        public async Task<IActionResult> StartTrain(string networkId)
        {
            //TODO: check if user realy owns the job
            if (await _neuralNetService.StartTrainNetwork(networkId))
                return Ok();
            else 
                return NotFound();
        }               

    }
}
