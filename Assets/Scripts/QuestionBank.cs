using System;

namespace DefaultNamespace
{
    public class QuestionBank
    {
        private Question[] m_QuestionBank = new Question[]
        {
            new (){
                QuestionText = "Which of these birds are not part of the Corvidae family?", 
                Options = new []{new OptionData{OptionText = "Crows"},new OptionData{OptionText = "Magpies"},new OptionData{OptionText = "Grackles", IsCorrect = true},new OptionData{OptionText = "Blue Jays"}}
            },
            new (){
                QuestionText = "Approximately how many times a day does the human heart beat?", 
                Options = new []{new OptionData{OptionText = "100,000", IsCorrect = true},new OptionData{OptionText = "1,000,000"},new OptionData{OptionText = "25,000"},new OptionData{OptionText = "200,000"}}
            },
            new (){
                QuestionText = "A prominent German philosopher once said \"The history of all hitherto existing society is the history of _________________\" ", 
                Options = new []{new OptionData{OptionText = "Exploitation"},new OptionData{OptionText = "Revolution"},new OptionData{OptionText = "False Consciousness"},new OptionData{OptionText = "Class Struggles", IsCorrect = true}}
            },
        };
        
        private Random m_Random;
        
        public void Init()
        {
            m_Random = new Random();
        }
        
        public Question GetQuestion()
        {
            int index = m_Random.Next(0, m_QuestionBank.Length);
            return m_QuestionBank[index];
        }
    }
}
