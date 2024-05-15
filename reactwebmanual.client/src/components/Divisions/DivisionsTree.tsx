import { FC, useMemo, Dispatch, SetStateAction } from 'react';
import { Tree, TreeDataNode } from "antd";

interface IAppProps {
    setSelectedDivisionId: Dispatch<SetStateAction<number | undefined>>;
    divisions: IDivisionsTreeNode[]
}

interface IDivisionsTreeNode {
    id: string,
    name: string,
    children: IDivisionsTreeNode[]
}

const transformTree = (treeNode: IDivisionsTreeNode): TreeDataNode => {
    const { id, name, children } = treeNode;
    const newTreeNode: TreeDataNode = {
        key: id,
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

const DivisionsTree: FC<IAppProps> = ({ setSelectedDivisionId, divisions }) => {

    const divisionsTree = useMemo(() => convertDataToAntdTreeFormat([...divisions]), [divisions])

    return (
        <Tree
            treeData={divisionsTree}
            onSelect={(e) => { setSelectedDivisionId(e[0] as number) }}
        />
    );

};

export default DivisionsTree;