using Learn.Classes.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace Learn.Classes
{
    public class MCQ
    {
        IDatabase database;
        public Card[] questions = new Card[4];
        public Card correctQ { get; set; }
        public Card[] incorrectQ { get; set; }
        public int correctQIndex { get; }

        public MCQ()
        {
            database = ConnectionManager.EstablishConnection(); database = ConnectionManager.EstablishConnection();
            incorrectQ = new Card[3];
        }
        public MCQ(Card correctQ, Card[] incorrectQ)
        {
            database = ConnectionManager.EstablishConnection(); database = ConnectionManager.EstablishConnection();
            this.correctQ = correctQ;
            this.incorrectQ = incorrectQ;
            questions[0] = correctQ;
            for (int i = 1; i < questions.Length; i++)
            {
                questions[i] = incorrectQ[i - 1];
            }
        }
        public void generateMCQ(Card correctCard)
        {
            string term = correctCard.term;
            string studySet = correctCard.group;
            correctQ = correctCard;

            DataRowCollection dummyChoices = database.GetQuery($"SELECT term, definition FROM TermCards WHERE term != \"{term}\" and grouping = \"{studySet}\" ORDER BY RANDOM() LIMIT 3").Rows;


            for (int i = 0;i < dummyChoices.Count;i++)
            {
                Card tempCard = new Card(dummyChoices[i].Field<string>("term"), dummyChoices[i].Field<string>("definition"), studySet);
                incorrectQ[i] = tempCard;
            }
        }

        public override string ToString()
        {
            return correctQ + " | " + incorrectQ[0] + incorrectQ[1] + incorrectQ[2];
        }
    }
}
