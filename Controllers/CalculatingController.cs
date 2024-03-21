using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SoftiusTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatingController : ControllerBase
    {
        [HttpPost("Solving/{studentCount}")]
        public JsonResult Solving (int studentCount, int[] messagesValue)
        {
            var result = GetResult(studentCount, messagesValue);
            return new JsonResult(result);
        }

        public static string GetResult(int studentCount, int[] messagesValue)
        {
            List<Tuple<int, int>> users = [];
            List<Tuple<int, int>> relationship = [];

            for (int i = 0; i < messagesValue.Length; i++)
                users.Add(Tuple.Create(i + 1, messagesValue[i]));

            if (studentCount != messagesValue.Length)
                return "Ошибка, количество студентов и количество общительности не совпадает";

            if (messagesValue.Sum() < studentCount - 1)
                return "-1";

            var firstUser = users.FirstOrDefault();
            if (firstUser == null || firstUser.Item2 == 0)
                return "-1";

            HashSet<int> usedValues = new HashSet<int>();
            for (int i = 0; i < firstUser.Item2; i++)
            {
                var nextUser = users.Where(x => x.Item1 != 1 && !usedValues.Contains(x.Item1)).OrderByDescending(x => x.Item2).FirstOrDefault();
                if (nextUser != null)
                {
                    relationship.Add(Tuple.Create(1, nextUser.Item1));
                    usedValues.Add(nextUser.Item1);
                }
                else
                    break;
            }
            users.Remove(firstUser);

            foreach (var user in users.Where(x => x.Item1 != 1).OrderByDescending(x => x.Item2).ToList())
            {
                for (int j = 0; j < user.Item2; j++)
                {
                    var user2 = users.Where(x => x.Item1 != 1 && x.Item1 != user.Item1 && !usedValues.Contains(x.Item1)).OrderByDescending(x => x.Item2).FirstOrDefault();
                    if (user2 != null)
                    {
                        relationship.Add(Tuple.Create(user.Item1, user2.Item1));
                        usedValues.Add(user2.Item1);
                    }
                    else
                        break;
                }
                users.Remove(user);
            }

            StringBuilder resultBuilder = new StringBuilder();
            
            if (relationship.Count > 0)
            {
                resultBuilder.AppendLine(relationship.Count.ToString());
                foreach (var rel in relationship)
                {
                    resultBuilder.AppendLine(rel.Item1 + " " + rel.Item2);
                }
            }

            return resultBuilder.ToString();
        }
    }
}
