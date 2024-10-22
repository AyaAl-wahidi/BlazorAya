using BlazorAya.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAya.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientContext _patientContext;

        public PatientsController(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        // GET: api/courses
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var courses = _patientContext.Select().Execute();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var patient = _patientContext.Select().Where(m => m.Eq(f => f.Id, id)).Execute().FirstOrDefault();

                if (patient == null)
                {
                    return NotFound($"Patient with id {id} not found.");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/courses
        [HttpPost]
        public IActionResult Add([FromBody] Patients course)
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }

            try
            {
                var result = _patientContext
                    .Insert()
                    .WithFields(m => m.Exclude(f => f.Id))
                    .Execute(course, returnNewRecord: true);

                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Patients course)
        {
            if (course == null)
            {
                return BadRequest("Course data is null.");
            }

            try
            {
                var updateResult = _patientContext
                    .Update()
                    .Where(m => m.Eq(f => f.Id, id))
                    .WithFields(m => m.ExcludeAll().FromField(f => f.Name).FromField(f => f.Address))
                    .Execute(course);

                if (updateResult == null)
                {
                    return NotFound($"Course with id {id} not found.");
                }

                var updatedCourse = _patientContext.Select().Where(m => m.Eq(f => f.Id, id)).Execute().FirstOrDefault();
                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _patientContext.Delete().Where(m => m.Eq(f => f.Id, id)).Execute();

                if (result == 0)
                {
                    return NotFound($"Course with id {id} not found.");
                }

                return NoContent(); // Return 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}