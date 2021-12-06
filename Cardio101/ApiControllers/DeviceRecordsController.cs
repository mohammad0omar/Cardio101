using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cardio101.Data;
using Cardio101.Models;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel.DataAnnotations;
using ExcelDataReader;
using System.ComponentModel;
using OfficeOpenXml;

namespace Cardio101.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceRecords
        [HttpGet]
        private async Task<ActionResult<IEnumerable<DeviceRecords>>> GetDeviceRecords()
        {
            return await _context.DeviceRecords.ToListAsync();
        }

        // GET: api/DeviceRecords/5
        [HttpGet("{id}")]
        private async Task<ActionResult<DeviceRecords>> GetDeviceRecords(int id)
        {
            var deviceRecords = await _context.DeviceRecords.FindAsync(id);

            if (deviceRecords == null)
            {
                return NotFound();
            }

            return deviceRecords;
        }

        // PUT: api/DeviceRecords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        private async Task<IActionResult> PutDeviceRecords(int id, DeviceRecords deviceRecords)
        {
            if (id != deviceRecords.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceRecords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceRecordsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DeviceRecords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost, DisableRequestSizeLimit]
        [Route("{studyId:int}")]
        public async Task<ActionResult> PostDeviceRecords([FromRoute()] int studyId,[Required] IFormFile file)
        {
            if (file.FileName.Split('.').Last() != "xls" && file.FileName.Split('.').Last() != "xlsx")
                throw new ValidationException("Please send an excel file to upload");
            if(studyId == 0)
            {
                throw new ValidationException("Please set a proper studyId");
            }
            Study study = _context.Study.FirstOrDefault(e => e.Id == studyId);
            if (study == null)
            {
                throw new ValidationException("Study Not Found");
            }
            if (study.Status == "Ended")
            {
                throw new ValidationException("Study Finished, cannot add more records");
            }
            DeviceRecords deviceRecord;

            var ms = new MemoryStream();
            file.CopyTo(ms);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                var cells = sheet.Cells;

                int itemscount = sheet.Dimension.End.Row;
                for (int i = 1; i <= itemscount; i++)
                {
                    deviceRecord = new DeviceRecords();
                    deviceRecord.Study = study;
                    UInt32 value;
                    if (UInt32.TryParse(cells[i, 2].Value.ToString(), out value))
                    {
                        deviceRecord.Value = value;
                    }
                    deviceRecord.Time = Convert.ToDateTime(cells[i, 1].Value);
                    _context.DeviceRecords.AddRange(deviceRecord);

                }
                await _context.SaveChangesAsync();

                return Ok(new { message = "Success"});
            }
          
        }

        // DELETE: api/DeviceRecords/5
        [HttpDelete("{id}")]
        private async Task<ActionResult<DeviceRecords>> DeleteDeviceRecords(int id)
        {
            var deviceRecords = await _context.DeviceRecords.FindAsync(id);
            if (deviceRecords == null)
            {
                return NotFound();
            }

            _context.DeviceRecords.Remove(deviceRecords);
            await _context.SaveChangesAsync();

            return deviceRecords;
        }

        private bool DeviceRecordsExists(int id)
        {
            return _context.DeviceRecords.Any(e => e.Id == id);
        }
    }
}
