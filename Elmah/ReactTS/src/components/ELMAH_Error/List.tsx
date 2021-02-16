import * as React from 'react'
import { ELMAH_Error } from 'src/features/ELMAH_Error/types';
import { IListProps } from 'src/framework/ViewModels/IListProps';
import ListItem from './ListItem';

export default function List(props: IListProps<ELMAH_Error>) {
    return (
        <div>
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.errorId} item={item} classes={props.classes} openFormInPopup={props.openFormInPopup} />
                );
            })}
        </div>
    );
}
