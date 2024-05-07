import { FC, useEffect, useMemo, useState, Dispatch, SetStateAction } from 'react';
import { Tree, TreeDataNode } from "antd";
import './Divisions.css';

interface IAppProps {
    setSelectedDivisions: Dispatch<SetStateAction<string | undefined>>;
}

interface IDivisionsTreeNode {
    id: string,
    parentId: string,
    name: string,
    children: IDivisionsTreeNode[]
}

const transformTree = (treeNode: IDivisionsTreeNode): TreeDataNode => {
    const { id, parentId, name, children } = treeNode;
    const newTreeNode: TreeDataNode = {
        key: `${parentId}-${id}`,
        title: name,
        children: []
    };

    if (children) {
        newTreeNode.children = children.map((child) => transformTree(child));
    }

    return newTreeNode;
};

const convertDataToAntdTreeFormat = (data: IDivisionsTreeNode[]): TreeDataNode[] => {
    const result: TreeDataNode[] = [];

    data.forEach((item) => {
        const antdDataTree = transformTree(item);
        result.push(antdDataTree);
    });
    
    return result;
};


const getDivisions = async (): Promise<IDivisionsTreeNode> => {
    try {
        const response = await fetch('https://localhost:7226/api/Division');
        const data = await response.json();
        return data;
    } catch {
        throw Error ('ошибка');
    }
};

const initialState: IDivisionsTreeNode[] = [];

const Divisions: FC<IAppProps> = () => {
    const [divisionsTree, setDivisionsTree] = useState<IDivisionsTreeNode[]>(initialState);


    useEffect(() => {
        (async () => {
            const result = await getDivisions();
            console.log('result on resp', result);
            setDivisionsTree([result])
        })();
    }, []);  
    
    const dataForTree = useMemo(() => convertDataToAntdTreeFormat([...divisionsTree]), [divisionsTree])

    return (
        <div className='division'>
            <h1>Подразделения</h1>
            <Tree
                treeData={dataForTree}
                onSelect={(e) => {
                    console.log('e', e);
                    // setSelectedDivisions('1')
                }}
            />
        </div>
    );

};
export default Divisions;