using AutoMapper;
using EMSApi.Data;
using EMSApi.IRepository;
using EMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _unitOfWork.Employee.GetAll();
                var sortEmployees = employees.OrderByDescending(x => x.FirstName);
                var results = _mapper.Map<IList<EmployeeDTO>>(sortEmployees);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetEmployees)}");
                return StatusCode(500, $"Internal Server Error. Please Try Again Later. {ex}");
            }
        }


        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            //throw new Exception();
            var employee = await _unitOfWork.Employee.Get(x => x.Id == id);
            var result = _mapper.Map<EmployeeDTO>(employee);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(CreateEmployee)}");
                return BadRequest(ModelState);
            }

            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                await _unitOfWork.Employee.Insert(employee);

                await _unitOfWork.Save();
                //return Ok();
                return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(CreateEmployee)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in the {nameof(UpdateEmployee)}");
                return BadRequest(ModelState);
            }

            try
            {
                var employee = await _unitOfWork.Employee.Get(x => x.Id == id);
                if (employee == null)
                {
                    _logger.LogError($"Invalid Update attempt in the {nameof(UpdateEmployee)}");
                    return BadRequest("Submited data is invalid");
                }

                _mapper.Map(createEmployeeDTO, employee);
                _unitOfWork.Employee.Update(employee);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateEmployee)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
