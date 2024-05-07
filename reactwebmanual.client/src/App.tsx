import { FC, useEffect, useId, useState } from 'react';
import './App.css';
import Workers from './components/Workers';
import Divisions from './components/Divisions';

interface IAppProps {

}


const App: FC<IAppProps> = ({ }) => {
    const [selectedDivisions, setSelectedDivisions] = useState<string>(); //указать тип

    return(
        <div className='content'>
            <Divisions
                setSelectedDivisions={setSelectedDivisions}
            />
            <Workers
                selectedDivisions={selectedDivisions}
            />
        </div>
    )
}
export default App;