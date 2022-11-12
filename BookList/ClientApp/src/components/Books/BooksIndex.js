import React, { useState } from "react";

export default function BooksIndex() {

    const [state, setState] = useState({ books: [], loading: true });

    function renderBooksTable(books) {
        return (

            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Created</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map(book => <tr>
                        <td>{book.Id}</td>
                        <td>{book.Title}</td>
                        <td>{book.Description}</td>
                        <td>{book.Created}</td>
                    </tr>)}
                </tbody>
            </table>
        )
    };

    let contents = state.loading ? <p><em>Loading...</em></p>
        : renderBooksTable(state.books);

    return (
        <>
            <h1>Data coding book List</h1>
            <h2>Publisehd by Richie 2022</h2>
            {contents}
        </>);
}