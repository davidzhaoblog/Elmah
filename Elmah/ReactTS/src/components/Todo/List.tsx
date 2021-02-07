import * as React from 'react'
import { IListProps } from 'src/framework/ViewModels/IListProps';
import ListItem from './ListItem';

export default function List(props: IListProps) {
    return (
        <div>
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.id} item={item} classes={props.classes} />
                );
            })}
        </div>
    );
}
