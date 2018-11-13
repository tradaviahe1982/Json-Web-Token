using System;
using System.Collections.Generic;
using System.Text;

namespace JWTDataAccess.Employee.Entity
{
    public class EmployeeEntity
    {
        //
        public EmployeeEntity() { }
        //
        private int id_;
        public int id
        {
            get { return id_; }
            set { id_ = value; }
        }
        //
        private string sothenv_;
        public string sothenv
        {
            get { return sothenv_; }
            set { sothenv_ = value; }
        }
        //
        private string tennv_;
        public string tennv
        {
            get { return tennv_; }
            set { tennv_ = value; }
        }
        //
        private string donvinv_;
        public string donvinv
        {
            get { return donvinv_; }
            set { donvinv_ = value; }
        }
        //
        private DateTime ngaykyhd_;
        public DateTime ngaykyhd
        {
            get { return ngaykyhd_; }
            set { ngaykyhd_ = value; }
        }
        //
    }
}
