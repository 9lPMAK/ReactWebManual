import { FC, useEffect, useId, useState } from 'react';
import './Divisions.css';

interface IAppProps {

}

interface IDivisions {
    id: number,
    parentID: number,
    name: string,
    createDate: number,
    descriptions: string,
}

const Divisions: FC<IAppProps> = ({ }) => {
    const [divisions, setDivisions] = useState<IDivisions[]>();


    useEffect(() => {
        getDivisions();
    }, []);


    const getDivisions = async () => {
        const response = await fetch('https://localhost:7226/api/Division');

        const data = await response.json();
        setDivisions(data);
    };

    return (
        <div className='division'>
            <h1>Подразделения</h1>
            <table >
             <thead>
                 <tr>
                     <th>name</th>
                     <th>createDate</th>
                     <th>descriptions</th>
                 </tr>
             </thead>
             <tbody>
                {divisions?.map(divisions =>
                    <tr key={divisions.id}>
                        <td>{divisions.name}</td>
                        <td>{divisions.createDate}</td>
                        <td>{divisions.descriptions}</td>
                    </tr>
                )}
            </tbody>
            </table>
        </div>
    );
};
export default Divisions;