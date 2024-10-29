import React, { useState } from 'react';
import './LoginForm.css';
import { FaUser, FaLock } from "react-icons/fa";
import { Link } from 'react-router-dom';

const LoginForm = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  const handleShowPassword = () => {
    setShowPassword(!showPassword);
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      const response = await fetch('https://localhost:7060/api/Admini/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
      });

      if (response.ok) {
        window.location.href = "/dashboard";
      } else {
        const errorMessage = await response.text();
        alert(errorMessage);
      }
    } catch (error) {
      console.error('Error logging in:', error);
      alert('An error occurred while logging in. Please try again later.');
    }
  }

  return (
    <div className='container'>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
        <div className='input-box'>
          <input type="text" placeholder='Email' required value={email} onChange={(e) => setEmail(e.target.value)} />
          <FaUser className='icon'/>
        </div>

        <div className='input-box'>
          <input type={showPassword ? "text" : "password"} placeholder='Password' required value={password} onChange={(e) => setPassword(e.target.value)} />
          <FaLock className='icon'/>
        </div>

        <div className='show-password'>
          <label>
            <input type="checkbox" checked={showPassword} onChange={handleShowPassword} />
            Show Password
          </label>
        </div>

        <button className='login-button' type='submit'>LOGIN</button>
      </form>

      <div className='register-link'>
        <p>Don't have an account? <Link to="/register">Register</Link></p>
      </div>
    </div>
  )
}

export default LoginForm;
