import { FC, useEffect, useId, useState } from 'react';
import './App.css';
import Workers from './components/Workers';
import Divisions from './components/Divisions';

// interface Worker {
//     id:number;
//     firstName: string;
//     lastName: string;
//     middleName: string;
//     dateBithday: number;
//     sex: string;
//     post: string;
//     driversLicense: boolean;
// }

// function App() {
//     const [worker, setWorker] = useState<Worker[]>();

//     useEffect(() => {
//         populateWeatherData();
//     }, []);

//     const contents = worker === undefined
//         ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://localhost:7226/api/Worker">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
//         : <table className="table table-striped" aria-labelledby="tabelLabel">
//             <thead>
//                 <tr>
//                     <th>firstName</th>
//                     <th>lastName</th>
//                     <th>middleName</th>
//                     <th>dateBithday</th>
//                     <th>sex</th>
//                     <th>post</th>
//                     <th>driversLicense</th>
//                 </tr>
//             </thead>
            // <tbody>
            //     {worker.map(worker =>
            //         <tr key={worker.id}>
            //             <td>{worker.firstName}</td>
            //             <td>{worker.lastName}</td>
            //             <td>{worker.middleName}</td>
            //             <td>{worker.dateBithday}</td>
            //             <td>{worker.sex}</td>
            //             <td>{worker.post}</td>
            //             <td>{worker.driversLicense}</td>
            //         </tr>
            //     )}
            //     <h1>Hello Romka</h1>
            // </tbody>
//         </table>;

//     return (
//         <div>
//             <h1 id="tabelLabel">Weather forecast</h1>
//             <p>This component demonstrates fetching data from the server.</p>
//             {contents}
//             <h1>Hello</h1>
//         </div>
//     );

//     async function populateWeatherData() {
//         const response = await fetch('https://localhost:7226/api/Worker', { mode: 'no-cors' });
//         const data = await response.json();
//         setWorker(data);
//     }
// }
// {workers?.map(x => {
//     return (<div>{x.lastName} {x.firstName} {x.middleName}</div>)
// }) ?? null}

interface IAppProps {

}


const App: FC<IAppProps> = ({ }) => {
    return(
        <div >
            <Divisions />
            <Workers />
        </div>
    )
}
export default App;