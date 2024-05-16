import { Dispatch, FC, FormEvent, SetStateAction, useCallback, useEffect, useMemo, useState } from "react";
import Modal from 'react-modal';
import { IDivision } from "../../models/IDivision";
import '../Modal.css';
import { ActionType } from "../../types/ActionType";

interface IDivisionModalProps {
    actionType: ActionType,
    setActionType: Dispatch<SetStateAction<ActionType>>;
    divisionId: number | undefined,
}

const DivisionModal: FC<IDivisionModalProps> = ({
    actionType,
    setActionType,
    divisionId
}) => {

    const [division, setDivision] = useState<IDivision>();
    const [visible, setVisible] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setDivision((prevState) => {
            if (!prevState)
                return prevState;

            return {
                ...prevState,
                [e.target.name]: e.target.value
            }
        });
    };

    const getAndSetDivisionById = async (id: number) => {
        try {
            const response = await fetch(`https://localhost:7226/api/Division/${id}`);
            const data = await response.json();
            setDivision(data);
        } catch {
            throw Error('ошибка');
        }
    };

    useEffect(() => {

        if (actionType == ActionType.Add) {
            const newDivision: IDivision = {
                id: 0,
                parentID: divisionId ?? 0,
                name: '',
                description: '',
            };
            setDivision(newDivision);

            setVisible(true);
            return;
        }

        if (actionType == ActionType.Edit && divisionId) {
            getAndSetDivisionById(divisionId);
            setVisible(true);
            return;
        }

        setVisible(false);
        setDivision(undefined);

    }, [actionType, divisionId])

    const closeModal = () => {
        setActionType(ActionType.None);
    };

    const formSubmit = useCallback(async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        e.stopPropagation();

        const response = await fetch(`https://localhost:7226/api/Division`, {
            method: actionType == ActionType.Edit ? 'PUT' : 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(division)
        });
        if (!response.ok) {
            alert('Не удалось добавить');
            return;
        }

        setActionType(ActionType.Refresh);

    }, [division, actionType]);

    const modalContentDiv = useMemo(() => {

        return (
            <div className='modalContent'>
                <button className="modalContentHeaderButton" onClick={closeModal}>Закрыть</button>
                <h2 >Добавить Подразделение</h2>
                <form className="modalContentForm" onSubmit={formSubmit}>
                    <p>Родительское подразделение</p>
                    <input name='parentID' required defaultValue={division?.parentID} onChange={handleChange}></input>
                    <p>Название</p>
                    <input name='name' required minLength={2} defaultValue={division?.name} onChange={handleChange} />
                    <p>Описание</p>
                    <input name='description' defaultValue={division?.description} onChange={handleChange} />

                    <button type='submit' className="modalContentButton">{
                        actionType == ActionType.Edit
                            ? 'Редактировать'
                            : 'Добавить'
                    }</button>
                </form>
            </div>
        );
    }, [formSubmit, division]);

    return (
        <Modal isOpen={visible} onRequestClose={closeModal} ariaHideApp={false}>
            {modalContentDiv}
        </Modal>
    )
};

export default DivisionModal;