// QuizQuestion.jsx
import React, { useState } from 'react';

function QuizQuestion({ question, options, onAnswerSubmit }) {
  const [selectedAnswer, setSelectedAnswer] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    onAnswerSubmit(selectedAnswer);
  };

  return (
    <div className="quiz-question">
      <h2>{question}</h2>
      <form onSubmit={handleSubmit}>
        {options.map((option, index) => (
          <div key={index}>
            <input
              type="radio"
              id={`option${index}`}
              name="answer"
              value={option}
              onChange={(e) => setSelectedAnswer(e.target.value)}
            />
            <label htmlFor={`option${index}`}>{option}</label>
          </div>
        ))}
        <button type="submit">Submit Answer</button>
      </form>
    </div>
  );
}

export default QuizQuestion;












// QuizContainer.jsx
import React, { useState } from 'react';
import QuizQuestion from './QuizQuestion';

function QuizContainer() {
  // Sample quiz data (in a real app, this would come from your backend)
  const quizData = [
    {
      id: 1,
      question: "What is the capital of France?",
      options: ["London", "Berlin", "Paris", "Madrid"],
      correctAnswer: "Paris"
    },
    {
      id: 2,
      question: "What is 2 + 2?",
      options: ["3", "4", "5", "6"],
      correctAnswer: "4"
    }
  ];

  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [score, setScore] = useState(0);
  const [isQuizFinished, setIsQuizFinished] = useState(false);

  const handleAnswer = (selectedAnswer) => {
    if (selectedAnswer === quizData[currentQuestion].correctAnswer) {
      setScore(score + 1);
    }

    if (currentQuestion + 1 < quizData.length) {
      setCurrentQuestion(currentQuestion + 1);
    } else {
      setIsQuizFinished(true);
    }
  };

  return (
    <div className="quiz-container">
      {!isQuizFinished ? (
        <>
          <div className="quiz-header">
            <h1>Simple Quiz</h1>
            <p>Question {currentQuestion + 1} of {quizData.length}</p>
            <p>Score: {score}</p>
          </div>
          <QuizQuestion 
            question={quizData[currentQuestion].question}
            options={quizData[currentQuestion].options}
            onAnswerSubmit={handleAnswer}
          />
        </>
      ) : (
        <div className="quiz-results">
          <h2>Quiz Complete!</h2>
          <p>Your final score: {score} out of {quizData.length}</p>
        </div>
      )}
    </div>
  );
}

export default QuizContainer;






/* QuizStyles.css */
.quiz-container {
  max-width: 600px;
  margin: 2rem auto;
  padding: 20px;
  background-color: #f8f9fa;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

.quiz-header {
  text-align: center;
  margin-bottom: 2rem;
}

.quiz-question {
  background: white;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 1rem;
}

.option-container {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin: 15px 0;
}

.option-button {
  padding: 10px 15px;
  border: 2px solid #007bff;
  border-radius: 5px;
  background: white;
  cursor: pointer;
  transition: all 0.3s ease;
}

.option-button:hover {
  background: #007bff;
  color: white;
}

.submit-button {
  width: 100%;
  padding: 12px;
  background: #28a745;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1.1rem;
  margin-top: 1rem;
}

.submit-button:hover {
  background: #218838;
}

.quiz-results {
  text-align: center;
  padding: 20px;
}

.progress-bar {
  width: 100%;
  height: 10px;
  background: #e9ecef;
  border-radius: 5px;
  margin: 1rem 0;
}

.progress-fill {
  height: 100%;
  background: #007bff;
  border-radius: 5px;
  transition: width 0.3s ease;
}
















// Timer.jsx
import React, { useEffect, useState } from 'react';

function Timer({ onTimeUp, seconds = 30 }) {
  const [timeLeft, setTimeLeft] = useState(seconds);

  useEffect(() => {
    if (timeLeft === 0) {
      onTimeUp();
      return;
    }

    const timer = setInterval(() => {
      setTimeLeft(prev => prev - 1);
    }, 1000);

    return () => clearInterval(timer);
  }, [timeLeft, onTimeUp]);

  return (
    <div className="timer">
      <div className="timer-bar" style={{ width: `${(timeLeft / seconds) * 100}%` }} />
      <span className="timer-text">{timeLeft}s</span>
    </div>
  );
}

// Add these new styles to QuizStyles.css


/* Add to QuizStyles.css */
@keyframes slideIn {
  from {
    transform: translateX(100%);
    opacity: 0;
  }
  to {
    transform: translateX(0);
    opacity: 1;
  }
}

@keyframes shake {
  0%, 100% { transform: translateX(0); }
  25% { transform: translateX(-10px); }
  75% { transform: translateX(10px); }
}

.timer {
  position: relative;
  height: 30px;
  background: #eee;
  border-radius: 15px;
  margin: 1rem 0;
  overflow: hidden;
}

.timer-bar {
  height: 100%;
  background: linear-gradient(90deg, #00ff00, #ff0000);
  transition: width 1s linear;
}

.timer-text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #333;
  font-weight: bold;
}

.question-enter {
  animation: slideIn 0.5s ease-out;
}

.correct-answer {
  background: #d4edda !important;
  border-color: #28a745 !important;
}

.wrong-answer {
  background: #f8d7da !important;
  border-color: #dc3545 !important;
  animation: shake 0.5s;
}
Now update your QuizContainer to include the timer:

// Updated QuizContainer.jsx
import React, { useState } from 'react';
import QuizQuestion from './QuizQuestion';
import Timer from './Timer';

function QuizContainer() {
  // ... previous state declarations ...
  const [showFeedback, setShowFeedback] = useState(false);

  const handleTimeUp = () => {
    if (!isQuizFinished) {
      handleAnswer(''); // Count as wrong answer if time runs out
    }
  };

  const handleAnswer = (selectedAnswer) => {
    const isCorrect = selectedAnswer === quizData[currentQuestion].correctAnswer;
    setShowFeedback(true);

    if (isCorrect) {
      setScore(score + 1);
    }

    // Show feedback for 1 second before moving to next question
    setTimeout(() => {
      setShowFeedback(false);
      if (currentQuestion + 1 < quizData.length) {
        setCurrentQuestion(currentQuestion + 1);
      } else {
        setIsQuizFinished(true);
      }
    }, 1000);
  };

  return (
    <div className="quiz-container">
      {!isQuizFinished ? (
        <>
          <div className="quiz-header">
            <h1>Simple Quiz</h1>
            <Timer onTimeUp={handleTimeUp} seconds={30} />
            <p>Question {currentQuestion + 1} of {quizData.length}</p>
            <p>Score: {score}</p>
          </div>
          <QuizQuestion 
            question={quizData[currentQuestion].question}
            options={quizData[currentQuestion].options}
            onAnswerSubmit={handleAnswer}
            showFeedback={showFeedback}
          />
        </>
      ) : (
        <div className="quiz-results">
          <h2>Quiz Complete!</h2>
          <p>Your final score: {score} out of {quizData.length}</p>
          <button 
            className="submit-button"
            onClick={() => window.location.reload()}
          >
            Try Again
          </button>
        </div>
      )}
    </div>
  );
}

export default QuizContainer;

