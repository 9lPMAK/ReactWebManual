import { FC, useEffect, useState, Dispatch, SetStateAction, useCallback } from 'react';
import DivisionsTree from './DivisionsTree';
import './index.css';
import DivisionModal from './DivisionModal';
import { ActionType } from '../../types/ActionType';
import { Button } from 'antd';
import {PlusSquareOutlined, FormOutlined , DeleteOutlined } from '@ant-design/icons';


interface IAppProps {
    selectedDivisionId: number | undefined;
    setSelectedDivisionId: Dispatch<SetStateAction<number | undefined>>;
}

interface IDivisionsTreeNode {
    id: string,
    parentId: string,
    name: string,
    children: IDivisionsTreeNode[]
}

const getDivisions = async (): Promise<IDivisionsTreeNode> => {
    try {
        const response = await fetch('https://localhost:7226/api/Division');
        const data = await response.json();
        return data;
    } catch {
        throw Error('ошибка');
    }
};

const initialState: IDivisionsTreeNode[] = [];

const Divisions: FC<IAppProps> = ({ selectedDivisionId, setSelectedDivisionId }) => {
    const [divisions, setDivisions] = useState<IDivisionsTreeNode[]>(initialState);
    const [actionType, setActionType] = useState(ActionType.Refresh);

    const refreshDivisions = async () => {
        const result = await getDivisions();
        setDivisions([result]);
        setActionType(ActionType.None);
    };

    useEffect(() => {
        if (actionType != ActionType.Refresh)
            return;

        refreshDivisions();
    }, [actionType]);

    const deleteDivision = useCallback(async () => {
        if (selectedDivisionId === undefined)
            return;

        const response = await fetch(`https://localhost:7226/api/Division/${selectedDivisionId}`, { method: 'DELETE' });
        if (!response.ok) {
            alert('Не удалось удалить');
            return;
        }

        setActionType(ActionType.Refresh);
    }, [selectedDivisionId]);

    return (
        <div className='division'>
            <h1>Подразделения</h1>
            <div className='buttons'>
                <Button onClick={() => setActionType(ActionType.Add)} icon={<PlusSquareOutlined />}/>
                <Button onClick={() => setActionType(ActionType.Edit)} icon={<FormOutlined />}/>
                <Button onClick={deleteDivision} icon={<DeleteOutlined />}/>
            </div>   
            <div className='divisionTree'>
                <DivisionsTree               
                    setSelectedDivisionId={setSelectedDivisionId}
                    divisions={divisions}
            />
            </div>

            <DivisionModal
                actionType={actionType}
                setActionType={setActionType}
                divisionId={selectedDivisionId}
            />
        </div>
    );
};
export default Divisions;