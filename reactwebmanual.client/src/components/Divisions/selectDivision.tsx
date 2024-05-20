import React, { useEffect, useMemo, useState, FC, Dispatch, SetStateAction } from 'react';
import { TreeSelect } from 'antd';
import { ActionType } from '../../types/ActionType';
import { DefaultOptionType } from 'antd/es/select';

interface IDivisionsTreeNode {
  id: string,
  parentId: string,
  name: string,
  children: IDivisionsTreeNode[]
}

interface ITreeSelectDivision {
  setValue: Dispatch<SetStateAction<string | undefined>>;
  value: string | undefined;
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

// const treeData = [
//   {
//     value: 'parent 1',
//     title: 'parent 1',
//     children: [
//       {
//         value: 'parent 1-0',
//         title: 'parent 1-0',
//         children: [
//           {
//             value: 'leaf1',
//             title: 'leaf1',
//           },
//           {
//             value: 'leaf2',
//             title: 'leaf2',
//           },
//           {
//             value: 'leaf3',
//             title: 'leaf3',
//           },
//           {
//             value: 'leaf4',
//             title: 'leaf4',
//           },
//           {
//             value: 'leaf5',
//             title: 'leaf5',
//           },
//           {
//             value: 'leaf6',
//             title: 'leaf6',
//           },
//         ],
//       },
//       {
//         value: 'parent 1-1',
//         title: 'parent 1-1',
//         children: [
//           {
//             value: 'leaf11',
//             title: <b style={{ color: '#08c' }}>leaf11</b>,
//           },
//         ],
//       },
//     ],
//   },
// ];

const initialState: IDivisionsTreeNode[] = [];


const TreeSelectDivision: FC<ITreeSelectDivision> = ({ setValue, value }) => {
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

  const onChange = (newValue: string) => {
    console.log('newValue', newValue)
    setValue(newValue);
  };

  const divisionsTree = useMemo(() => convertDataToAntdTreeFormat(divisions), [divisions])

  return (
    <TreeSelect
      showSearch
      style={{ width: '100%' }}
      value={value}
      dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
      placeholder="Please select"
      allowClear
      treeDefaultExpandAll
      onChange={onChange}
      treeData={divisionsTree}
    />
  );
};

export default TreeSelectDivision;