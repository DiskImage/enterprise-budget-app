using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using EnterpriseBudgetApp.Models;

namespace EnterpriseBudgetApp.Controllers.BLL
{
    public class GroupLogic
    {
        private vm343_01aEntities db = null;

        public GroupLogic()
        {
            this.db = new vm343_01aEntities();
        }

        public bool createGroup(Group group)
        {
            bool success = false;
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;

            db.Groups.Add(group);
            this.addGroupMember(group, currentUserId, 0); //0 is super-admin roleId.
            db.SaveChanges();

            return success;
        }

        private void addGroupMember(Group group, int userId, int roleId)
        {
            Group_Users gu = new Group_Users();
            gu.GroupId = group.GroupId;
            gu.UserId = userId;
            gu.RoleId = roleId;
            gu.deleted = false;
            db.Group_Users.Add(gu);
        }

        public void removeGroupMember(Group group, int userId)
        {
            var id = group.GroupId;
            var query = from p in db.Group_Users
                        where p.GroupId == id
                        where p.UserId == userId
                        select p;
            IEnumerable<Group_Users> grp_usrs = query.ToList();
            foreach (Group_Users to_kill in grp_usrs)
            {
                db.Group_Users.Remove(to_kill);
            }
        }

        public void deleteGroup(int id)
        {
            Group group = db.Groups.Find(id);
            //var id = group.GroupId;
            db.Groups.Remove(group);
            var query = from p in db.Group_Users
                        where p.GroupId == id
                        select p;
            IEnumerable<Group_Users> grp_usrs = query.ToList();
            foreach (Group_Users to_kill in grp_usrs)
            {
                db.Group_Users.Remove(to_kill);
            }
            db.SaveChanges();
        }
    }
}