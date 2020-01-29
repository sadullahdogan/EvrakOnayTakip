using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccsessLayer.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public DateTime Time { get; set; }
        public string FileName { get; set; }
        public string Details { get; set; }
        public RequestState RequestState { get; set; }
        public string Result { get; set; }
        public int UserId { get; set; }
        public  User User { get; set; }
    }
    public enum RequestState {Created,Accepted,Declined }
}
