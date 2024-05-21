import { useEffect, useMemo, useState, FC} from 'react';
import { TreeSelect } from 'antd';
import { DefaultOptionType } from 'antd/es/select';

interface IDivisionsTreeNode {
  id: string,
  parentId: string,
  name: string,
  children: IDivisionsTreeNode[]
}

interface ITreeSelectDivision {
  onSelect: (divisionId: string | undefined) => void;
  value: number | undefined;
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

const transformTree = (treeNode: IDivisionsTreeNode): DefaultOptionType => {
  const { id, name, children } = treeNode;
  const newTreeNode: DefaultOptionType = {
    value: id,
    title: name,
    children: []
  };

  if (children) {
    newTreeNode.children = children.map((child) => transformTree(child));
  }

  return newTreeNode;
};

const convertDataToAntdTreeFormat = (data: IDivisionsTreeNode[]): DefaultOptionType[] => {

  const result: DefaultOptionType[] = [];

  data.forEach((item) => {
    const antdDataTree = transformTree(item);
    result.push(antdDataTree);
  });

  return result;
};

const initialState: IDivisionsTreeNode[] = [];

const TreeSelectDivision: FC<ITreeSelectDivision> = ({ onSelect, value }) => {
  const [divisions, setDivisions] = useState<IDivisionsTreeNode[]>(initialState);

  const refreshDivisions = async () => {
    const result = await getDivisions();
    setDivisions([result]);
  };

  useEffect(() => {
    refreshDivisions();
  }, []);

  const divisionsTree = useMemo(() => convertDataToAntdTreeFormat(divisions), [divisions])

  return (
    <TreeSelect
      showSearch
      style={{ width: '100%' }}
      value={value as unknown as string}
      dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
      placeholder="Please select"
      allowClear
      treeDefaultExpandAll
      onChange={onSelect}
      treeData={divisionsTree}
    />
  );
};

export default TreeSelectDivision;