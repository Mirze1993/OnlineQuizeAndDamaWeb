using MicroORM;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{

    public class QuizeRepository:CRUD<Quize>
    {
        public List<UICategory> GetAllCategory(int id=0)
        {
            using (var comander=DBContext.CreateCommander())
            {
                return comander.Reader<UICategory>("GetAllCategory",
                    commandType:System.Data.CommandType.StoredProcedure
                    , parameters:new List<System.Data.Common.DbParameter> { 
                    comander.SetParametr("userId",id)
                    }).Value;
            }
        }

        public List<KeyValue> GetQuizeWriterUsers(int subjectid)
        {
            using (var comander = DBContext.CreateCommander())
            {
                return comander.Reader<KeyValue>("getQuizeWriterUsers",
                    commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                    comander.SetParametr("subjectId",subjectid)
                    }).Value;
            }
        }

        public List<UISelectLesson> GetStudentTeacherByStudentId(int studentId)
        {
            using (var comander = DBContext.CreateCommander())
            {
                return comander.Reader<UISelectLesson>("GetStudentTeacherByStudentId",
                    commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                    comander.SetParametr("studentId",studentId)
                    }).Value;
            }
        }

       
        public List<Category> SelectCategoryInGroup(int groupeId)
        {
            using (var comander = DBContext.CreateCommander())
            {
                return comander.Reader<Category>("SelectCategoryInGroup",
                    commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                    comander.SetParametr("groupeId",groupeId)
                    }).Value;
            }
        }

        public List<UIResultQuize> GetResults(int studentId,int teacherId)
        {
            using (var comander = DBContext.CreateCommander())
            {
                return comander.Reader<UIResultQuize>("GetResults",
                    commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                    comander.SetParametr("studentId",studentId),
                    comander.SetParametr("teacherId",teacherId),
                    }).Value;
            }
        }

    }
}
