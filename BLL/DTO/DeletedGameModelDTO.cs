using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class DeletedGameModelDTO
    {
        public string UserId { get; set; }
        public List<int> deletedGamesID { get; set; }
    }
}
