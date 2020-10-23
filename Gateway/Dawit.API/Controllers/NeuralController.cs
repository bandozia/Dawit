﻿using Dawit.Domain.Model;
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
        private readonly IMsgProducer _eventProducer;

        public NeuralController(IMsgProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        [HttpPost("train")]
        public IActionResult SendToTrain()
        {
            var testJob = new NeuralJob { Id = Guid.NewGuid(), Name = "Job teste" };
            if (_eventProducer.AddEventToQueue<NeuralJob>(Queues.NN_START_TRAIN, testJob))
            {
                return Ok("evento enviado!");
            }
            else
            {
                return Ok("a fila nao foi criada");
            }
            
        }
    }
}
