using System;
using System.Text;
using System.Collections.Generic;


namespace NHibernateDemo.Domain
{

    public class Visitor
    {
        public virtual int VisitorId { get; set; }
        //public virtual Visit Visit { get; set; }
        //public virtual Employee Employee { get; set; }
        //public virtual Contact Contact { get; set; }
        public virtual int IsPrimaryVisitor { get; set; }
        public virtual string VisitorCompany { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateCheckedIn { get; set; }
        public virtual DateTime DateCheckedOut { get; set; }
        public virtual DateTime DateBadgePrinted { get; set; }
        public virtual int TmpVisitorId { get; set; }
        public virtual int RestrictionType { get; set; }
        public virtual string Picture { get; set; }
        public virtual string RestrictionNotes { get; set; }
        public virtual string SecuritySystemVisitorId { get; set; }
        public virtual string SecuritySystemBarCode { get; set; }
        public virtual int PhotoLength { get; set; }
        public virtual byte[] PhotoData { get; set; }
        public virtual string VisitorEmail { get; set; }
        public virtual string Pin { get; set; }
        public virtual int CheckInSource { get; set; }
        public virtual string VisitorFirstName { get; set; }
        public virtual string VisitorLastName { get; set; }
        public virtual string VisitorName { get; set; }
        public virtual string SecuritySystemVisitId { get; set; }
        public virtual int AngusIntegrationVisitorId { get; set; }
        public virtual int IsSyncFromIntegration { get; set; }
        public virtual int VisitorCsCompanyId { get; set; }
        public virtual int VisitorType { get; set; }
        public virtual DateTime DateUpdated { get; set; }
    }
}
