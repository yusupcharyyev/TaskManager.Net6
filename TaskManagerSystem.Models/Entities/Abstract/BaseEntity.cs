using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Enums;

namespace TaskManagerSystem.Models.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        private DateTime _createDate = DateTime.Now;

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? RemovedDate { get; set; }

        private Statu _statu = Statu.Active;

        public Statu Statu
        {
            get { return _statu; }
            set { _statu = value; }
        }

    }
}
