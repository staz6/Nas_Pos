using System;
using System.ComponentModel.DataAnnotations;

namespace Nas_Pos.Entities.Identity
{
    public class BaseClass
    {
        
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}