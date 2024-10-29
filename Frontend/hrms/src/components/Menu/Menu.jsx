import React from 'react'
import { Link } from 'react-router-dom';
import { BiHome, BiBookAlt, BiSolidReport, BiStats, BiTask, BiMessage } from 'react-icons/bi';
import './Menu.css';


const Menu = () => {
    return (
        <div className='menu'>
            <div className="log">
                <BiBookAlt className='logo-icon' />
                <h2>HRMS Logo</h2>
            </div>
            <div className="menu-list">
                <Link to="/dashboard" className='item'>
                    <BiHome className='icon' /> Dashboard
                </Link>
            </div>
            <div className="menu-list">
                <Link to="/employee" className='item'>
                    <BiTask className='icon' /> Employees
                </Link>
            </div>
            <div className="menu-list">
                <Link to="/department" className='item'>
                    <BiTask className='icon' /> Departments
                </Link>
            </div>
            <div className="menu-list">
                <Link to="/leave" className='item'>
                    <BiMessage className='icon' /> Leave
                </Link>
            </div>
        </div>
    )
}

export default Menu
