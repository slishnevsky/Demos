using System;
using System.Text;
using System.Collections.Generic;


namespace NHibernateDemo.Domain
{

    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
