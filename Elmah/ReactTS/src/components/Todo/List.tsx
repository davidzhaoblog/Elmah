import * as React from 'react'
import { Todo } from 'src/features/Todo/types';
import { IListProps } from 'src/framework/ViewModels/IListProps';
import ListItem from './ListItem';

export default function List(props: IListProps<Todo>) {
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
