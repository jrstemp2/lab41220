using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SlackOverload.Models
{
    public class DAL
    {
        private SqlConnection conn;

        public DAL(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        public int CreateQuestion(Question q)
        {
            q.Posted = DateTime.Now;
            q.Status = 1; //always create status=1

            string addQuery = "INSERT INTO Questions (Username, Title, Detail, Posted, Category, Tags, Status) ";
            addQuery += "VALUES (@Username, @Title, @Detail, @Posted, @Category, @Tags, @Status)";

            return conn.Execute(addQuery, q);
        }

        public IEnumerable<Answer> GetAnswersByQuestionId(int id)
        {
            string queryString = "SELECT * FROM Answers WHERE QuestionId = @id";
            return conn.Query<Answer>(queryString, new { id = id });
        }

        public Question GetQuestionById(int id)
        {
            string queryString = "SELECT * FROM Questions WHERE Id = @id";
            return conn.QueryFirstOrDefault<Question>(queryString, new { id = id });
        }

        public IEnumerable<Question> GetQuestionsMostRecent()
        {
            string queryString = "SELECT TOP 20 * FROM Questions ORDER BY Posted DESC";
            return conn.Query<Question>(queryString);
        }

        public int UpdateQuestionById(Question q)
        {
            q.Posted = DateTime.Now;
            string editString = "UPDATE Questions SET Username = @Username, Title = @Title, Detail = @Detail, Category = @Category, Posted = @Posted, Tags = @Tags, Status = @Status WHERE Id = @Id";
            return conn.Execute(editString, q);
        }




        public int AddAnswer(Answer a, int id)
        {
            a.Posted = DateTime.Now;
            //q.Status = 1; //always create status=1            
            string addQuery = "INSERT INTO Answers (Username, Detail, QuestionId, Posted, UpVotes) ";
            addQuery += "VALUES (@Username, @Detail, @Id, @Posted, @UpVotes)";

            return conn.Execute(addQuery, a);
        }


        //update answer by id
        //find answer by id
        public Answer GetAnswerById(int id)
        {
            string queryString = "SELECT * FROM Answers WHERE Id = @id";
            return conn.QueryFirstOrDefault<Answer>(queryString, new { id = id });
        }
        public int UpdateAnswerById(Answer a)
        {
            a.Posted = DateTime.Now;
            string editString = "UPDATE Answers SET Username = @Username, Detail = @Detail, Upvotes = @UpVotes WHERE Id = @Id";
            return conn.Execute(editString, a);
        }

        public IEnumerable<Question> GetQuestionByKeyWord(string searchTerm)
        {
            
            string queryString = "SELECT * FROM Questions WHERE Category = @val";

            IEnumerable<Question> questions = conn.Query<Question>(queryString, new { val = searchTerm});



            return questions;
        }
    }
}
