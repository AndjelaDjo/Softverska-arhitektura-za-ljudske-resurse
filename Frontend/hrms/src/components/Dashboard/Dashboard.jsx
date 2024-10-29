import React, { useEffect, useState } from 'react';
import './Dashboard.css';
import axios from 'axios';
import { Pie, Bar } from 'react-chartjs-2';
import 'chart.js/auto';

const Dashboard = ({ onRequestClose }) => {
    const [employeeMaleCount, setEmployeeMaleCount] = useState(0);
    const [employeeFemaleCount, setEmployeeFemaleCount] = useState(0);
    const [avgWomanAge, setAvgWomanAge] = useState(0);
    const [avgManAge, setAvgManAge] = useState(0);
    const [departmentData, setDepartmentData] = useState([]);

    useEffect(() => {
        const fetchEmployeeData = async () => {
            try {
                const countMaleResponse = await axios.get('https://localhost:7060/api/Zaposleni/numOfMen');
                setEmployeeMaleCount(countMaleResponse.data);

                const countFemaleResponse = await axios.get('https://localhost:7060/api/Zaposleni/numOfWomen');
                setEmployeeFemaleCount(countFemaleResponse.data);

                const avgWomanResponse = await axios.get('https://localhost:7060/api/Zaposleni/avgYearsOfWomen');
                setAvgWomanAge(avgWomanResponse.data);

                const avgManResponse = await axios.get('https://localhost:7060/api/Zaposleni/avgYearsOfMen');
                setAvgManAge(avgManResponse.data);

                const departmentResponse = await axios.get('https://localhost:7060/api/Odeljenja/getAllOdeljenja');
                const departmentPromises = departmentResponse.data.map(department =>
                    axios.get(`https://localhost:7060/api/Odeljenja/getNumOfZaposlenihPoOdeljenju/${department.naziv}`)
                        .then(response => ({ name: department.naziv, count: response.data }))
                );
                const departments = await Promise.all(departmentPromises);
                setDepartmentData(departments);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchEmployeeData();
    }, []);

    const pieData = {
        labels: ['Male', 'Female'],
        datasets: [
            {
                data: [employeeMaleCount, employeeFemaleCount],
                backgroundColor: ['#36A2EB', '#FF6384'],
                hoverBackgroundColor: ['#36A2EB', '#FF6384'],
            },
        ],
    };

    const pieOptions = {
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var dataset = data.datasets[tooltipItem.datasetIndex];
                    var dataPoint = dataset.data[tooltipItem.index];
                    var labels = data.labels[tooltipItem.index];
                    return labels + ': ' + ((dataPoint / dataset.data.reduce((a, b) => a + b, 0)) * 100).toFixed(2) + '%';
                }
            }
        },
        plugins: {
            tooltip: {
                callbacks: {
                    label: function (context) {
                        let label = context.label || '';
    
                        if (context.datasetIndex !== undefined && context.dataIndex !== undefined) {
                            let value = context.dataset.data[context.dataIndex];
                            label += ': ' + ((value / context.dataset.data.reduce((a, b) => a + b, 0)) * 100).toFixed(2) + '%';
                        }
                        return label;
                    }
                }
            }
        }
    };
    
    
    const barData = {
        labels: departmentData.map(dep => dep.name),
        datasets: [
            {
                label: 'Number of Employees',
                data: departmentData.map(dep => dep.count),
                backgroundColor: 'rgba(75, 192, 192, 0.6)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1,
            },
        ],
    };

    const barOptions = {
        indexAxis: 'y',
        scales: {
            x: {
                position: 'bottom',
                offset: true,
                grid: {
                    offset: true
                }
            },
            y: {

            }
        },
        layout: {
            padding: {
                left: 50,
                right: 30
            },
            margin: {
                left: 30

            }
        }
    };



    return (
        <div className='dashboard-container'>
            <div className="dashboard-row">
                <div className='dashboard-card'>
                    <b><p className='label'>Total Male Employees</p></b>
                    <p className='number'>{employeeMaleCount}</p>
                </div>
                <div className='dashboard-card'>
                    <b><p className='label'>Total Female Employees</p></b>
                    <p className='number'>{employeeFemaleCount}</p>
                </div>
                <div className='dashboard-card'>
                    <b><p className='label'>Average Age of Women</p></b>
                    <p className='number'>{avgWomanAge}</p>
                </div>
                <div className='dashboard-card'>
                    <b><p className='label'>Average Age of Men</p></b>
                    <p className='number'>{avgManAge}</p>
                </div>
            </div>
            <div className="dashboard-row">
                <div className="dashboard-chart">
                    <Pie data={pieData} options={pieOptions} />
                </div>
                <div className="dashboard-chart" style={{ width: '800px', height: '300px', marginLeft: '50px' }}>
                    <Bar data={barData} options={barOptions} />
                </div>
            </div>
        </div>
    );
}

export default Dashboard;
