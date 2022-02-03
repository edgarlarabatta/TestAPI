using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Interfaces.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ActivityController> _logger;
        private readonly IActivityServices service;

        public ActivityController(ILogger<ActivityController> logger, IActivityServices service)
        {
            _logger = logger;
            this.service = service;
        }

        /// <summary>
        /// EndPoint que retorna el listado de los registros existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<IEnumerable<Test.Entities.Activity>>> Get()
        {
            IEnumerable<Test.Entities.Activity> response = null;
            try
            {
                response = await this.service.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        /// <summary>
        /// EndPoint que permite agregar una nueva actividad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Test.Entities.Activity>> Post(Test.Entities.Activity entity)
        {
            Test.Entities.Activity response = null;
            try
            {
                if (ModelState.IsValid)
                {
                    response = await this.service.Add(entity);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, errors = ex.Message });
            }
        }

        /// <summary>
        /// EndPoint que permite reagendar una actividad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reschedule")]
        public async Task<ActionResult<Test.Entities.Activity>> Reschedule(Test.Entities.Activity entity)
        {
            Test.Entities.Activity response = null;
            try
            {
                if (ModelState.IsValid)
                {
                    response = await this.service.Reschedule(entity);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, errors = ex.Message });
            }
        }

        /// <summary>
        /// EndPoint que permite cancelar actividad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("cancel")]
        public async Task<ActionResult<Test.Entities.Activity>> Cancel(Test.Entities.Activity entity)
        {
            Test.Entities.Activity response = null;
            try
            {
                if (ModelState.IsValid)
                {
                    response = await this.service.Cancel(entity);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, errors = ex.Message });
            }
        }


        /// <summary>
        /// EndPoint que permite finalizar una actividad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("terminate")]
        public async Task<ActionResult<Test.Entities.Activity>> Terminate(Test.Entities.Activity entity)
        {
            Test.Entities.Activity response = null;
            try
            {
                if (ModelState.IsValid)
                {
                    response = await this.service.Terminate(entity);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, errors = ex.Message });
            }
        }




        /// <summary>
        /// EndPoint que retorna el listado de los registros existentes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getbyfilters")]
        public async Task<ActionResult<IEnumerable<Test.Entities.Activity>>> GetByFilters(Test.Entities.Parameters parameters)
        {
            IEnumerable<Test.Entities.Activity> response = null;
            try
            {
                response = await this.service.GetByFilters(parameters);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}
