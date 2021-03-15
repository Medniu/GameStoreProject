using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class DeletedGameDTO
    {
        public string UserId { get; set; }
        public List<int> deletedGamesID { get; set; }
    }
}
