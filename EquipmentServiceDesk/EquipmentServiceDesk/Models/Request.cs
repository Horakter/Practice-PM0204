using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentServiceDesk.Models
{
    public class Request
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public string Status {get;set;}
        public DateTime CreatedAt {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int EquipmentId {get;set;}
        public Equipment Equipment {get;set;}
    }
}
