using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PersistentUnreal.Data;

namespace PersistentUnreal.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ItemContext m_Context;

        public ItemController(ItemContext context)
        {
            m_Context = context;

            if(m_Context.Items.Count() == 0)
            {
                m_Context.Items.Add(new ItemRecord { ItemName = "TestItem", Price = 100 });
                m_Context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<ItemRecord> GetAll()
        {
            return m_Context.Items.ToList();
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetById(long id)
        {
            var item = m_Context.Items.FirstOrDefault(r => r.Id == id);
            if(item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemRecord item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            m_Context.Items.Add(item);
            m_Context.SaveChanges();

            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ItemRecord item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var existingItem = m_Context.Items.FirstOrDefault(t => t.Id == id);
            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.ItemName = item.ItemName;
            existingItem.Price = item.Price;

            m_Context.Items.Update(existingItem);
            m_Context.SaveChanges();

            return new NoContentResult();
        }
    }
}
