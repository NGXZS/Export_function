import React, { useState, useEffect } from 'react';
import { CSVLink } from "react-csv"; //npm i react-csv
import "./ExportCSV.css";  // see https://surajsharma.net/blog/react-create-csv-file

const ExportCSV = () => {
    const headers = [ // key must follow key of data, label is wtv you want
        { label: 'Id', key: 'id' }, 
        { label: 'StartDis', key: 'startDis' },
        { label: 'EndDis', key: 'endDis' },
        { label: 'string1', key: 'string1' },
        { label: 'string2', key: 'string2' }
    ];

    // fetched data must be [] or string to use {CSVLink}
    const [fileData, setFileData] = useState([]); //initialize to empty []
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(1);
    const [CSVReport, setCSVReport] = useState({
        filename: 'report.csv',
        headers: headers,
        data: fileData
    });
/*
    useEffect(() => { 
        setCSVReport();
    },[fileData]);
*/
    const handleDataFetch = () => { 
        // `https://localhost:44365/api/Book?PageNumber=1&PageSize=${pageSize}` 
        // "https://localhost:44365/api/Book?PageNumber=1&PageSize=3"
        return fetch(`https://localhost:44365/api/Book?PageNumber=${pageNumber}&PageSize=${pageSize}`)
        .then(response => response.json())
        .then((json) => {
            const arr = [];
            json.data.map(elem => arr.push(elem));
            // console.log("the arr is :");
            // console.log(arr);
            setFileData(arr);
            console.log("pageSize is:");
            console.log(pageSize);
            handleCSVReport();
            //reset setPageNumber & setPageSize
        });
    }  
    const handleCSVReport = () => {
        setCSVReport({
            ...CSVReport, //copies old fields
            data: fileData});
        console.log("fileData is : ")
        console.log(fileData);
    }
   
    return (
        <div>
            <h3>Export to CSV</h3>
            <h2>Reference link. </h2>
            <label>Page Size: </label>
            <input placeholder="pageSize" value={pageSize} onChange={(e) => setPageSize(e.target.value)} />
            <label>Page Number: </label>
            <input placeholder="pageNumber" value={pageNumber} onChange={(e) => setPageNumber(e.target.value)} />
            <button onClick={handleDataFetch}>Confirm</button>
            <CSVLink onClick={handleDataFetch}{...CSVReport}>
            <button onClick={handleCSVReport}>Export</button>
            </CSVLink>       
        </div >
    );
}

export default ExportCSV; // call <ExportCSV /> in App.js*
