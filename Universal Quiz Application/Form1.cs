using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Universal_Quiz_Application
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswer { get; set; }

        public Question(string questionText, string[] options, int correctAnswer)
        {
            QuestionText = questionText;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }

    public partial class Form1 : Form
    {
        private List<Question> questions;
        private int currentQuestion = 0;
        private int score = 0;

        // Controls
        private Label lblQuestion;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Button btnSubmit;
        private Label lblScore;

        public Form1()
        {
            InitializeComponent();
        }

        // Add this method to handle Form1_Load
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeQuiz();
            SetupControls();
            DisplayQuestion();
        }

        private void InitializeQuiz()
        {
            questions = new List<Question>();
            questions.Add(new Question(
                "What is the capital of France?",
                new string[] { "London", "Berlin", "Paris", "Madrid" },
                2
            ));
            // Add more questions here
        }

        private void SetupControls()
        {
            // Initialize Question Label
            lblQuestion = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(540, 40),
                Text = "Question Goes Here",
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            // Initialize Radio Buttons
            radioButton1 = new RadioButton
            {
                Location = new Point(20, 70),
                Size = new Size(540, 30),
                Text = "Option 1"
            };

            radioButton2 = new RadioButton
            {
                Location = new Point(20, 110),
                Size = new Size(540, 30),
                Text = "Option 2"
            };

            radioButton3 = new RadioButton
            {
                Location = new Point(20, 150),
                Size = new Size(540, 30),
                Text = "Option 3"
            };

            radioButton4 = new RadioButton
            {
                Location = new Point(20, 190),
                Size = new Size(540, 30),
                Text = "Option 4"
            };

            // Initialize Submit Button
            btnSubmit = new Button
            {
                Text = "Submit",
                Location = new Point(20, 240),
                Size = new Size(540, 30)
            };
            btnSubmit.Click += BtnSubmit_Click;

            // Initialize Score Label
            lblScore = new Label
            {
                Text = "Score: 0",
                Location = new Point(20, 280),
                Size = new Size(540, 30),
                Font = new Font("Arial", 12)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblQuestion,
                radioButton1,
                radioButton2,
                radioButton3,
                radioButton4,
                btnSubmit,
                lblScore
            });
        }

        private void DisplayQuestion()
        {
            if (currentQuestion < questions.Count)
            {
                lblQuestion.Text = questions[currentQuestion].QuestionText;
                radioButton1.Text = questions[currentQuestion].Options[0];
                radioButton2.Text = questions[currentQuestion].Options[1];
                radioButton3.Text = questions[currentQuestion].Options[2];
                radioButton4.Text = questions[currentQuestion].Options[3];
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Check if any option is selected
            if (!radioButton1.Checked && !radioButton2.Checked &&
                !radioButton3.Checked && !radioButton4.Checked)
            {
                MessageBox.Show("Please select an answer!", "Warning");
                return;
            }

            // Get selected answer
            int selectedAnswer = -1;
            if (radioButton1.Checked) selectedAnswer = 0;
            if (radioButton2.Checked) selectedAnswer = 1;
            if (radioButton3.Checked) selectedAnswer = 2;
            if (radioButton4.Checked) selectedAnswer = 3;

            // Check answer
            if (selectedAnswer == questions[currentQuestion].CorrectAnswer)
            {
                score++;
                MessageBox.Show("Correct!");
            }
            else
            {
                MessageBox.Show("Incorrect!");
            }

            // Update score display
            lblScore.Text = $"Score: {score}";

            // Move to next question
            currentQuestion++;
            if (currentQuestion < questions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                MessageBox.Show($"Quiz complete! Final score: {score}/{questions.Count}");
                // Reset quiz
                currentQuestion = 0;
                score = 0;
                DisplayQuestion();
            }
        }
    }
}
