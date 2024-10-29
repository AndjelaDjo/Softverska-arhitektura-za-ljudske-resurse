import React, { useState } from 'react';
import './RegisterForm.css';
import { FaUser, FaLock } from "react-icons/fa";
import { Link } from 'react-router-dom';

function RegisterForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [showPassword, setShowPassword] = useState(false);

    const handleShowPassword = () => {
        setShowPassword(!showPassword);
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return;
        }
    
        try {
            const response = await fetch('https://localhost:7060/api/Admini', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ email, password })
            });
    
            if (response.ok) {
                alert("Registration successful!");
                window.location.href = "/dashboard"; 
            } else {
                alert("Registration failed. Please try again.");
            }
        } catch (error) {
            console.error('Error registering:', error);
            alert('An error occurred while registering. Please try again later.');
        }
    }
    

    return (
        <div className='container'>
            <h1>Registration</h1>
            <form onSubmit={handleSubmit}>
                <div className='input-box'>
                    <input type="text" placeholder='Email' required value={email} onChange={(e) => setEmail(e.target.value)} />
                    <FaUser className='icon' />
                </div>

                <div className='input-box'>
                    <input type={showPassword ? "text" : "password"} placeholder='Password' required value={password} onChange={(e) => setPassword(e.target.value)} />
                    <FaLock className='icon' />
                </div>

                <div className='input-box'>
                    <input type={showPassword ? "text" : "password"} placeholder='Confirm Password' required value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
                    <FaLock className='icon' />
                </div>

                <div className='show-password'>
                    <label>
                        <input type="checkbox" checked={showPassword} onChange={handleShowPassword} />
                        Show Password
                    </label>
                </div>

                <button className='register-button' type='submit'>REGISTER</button>
            </form>

            <div className='register-link'>
                <p>Already have an account? <Link to="/login">Login</Link></p>
            </div>
        </div>
    )
}

export default RegisterForm;
