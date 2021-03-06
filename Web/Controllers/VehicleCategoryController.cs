using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleCategoryController : CrudController<VehicleCategory>
    {
        public VehicleCategoryController(
            ILogger<VehicleCategory> logger, IBaseService<VehicleCategory> service
        ) : base(logger, service) {}

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Create([FromBody] VehicleCategory vehicleCategory)
        {
            return base.Create(vehicleCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Update(string id, [FromBody] VehicleCategory vehicleCategory)
        {
            return base.Update(id, vehicleCategory);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IStatusCodeActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
