import * as React from 'react';

export default class Counter extends React.Component {
    constructor() {
        super();
        this.state = { currentCount: 0 };
    }
    render() {
        return <div>
            <h1>Counter</h1>
            <p>This is a simple example of a React component.</p>
            <p>Current count: <strong>{ this.props.counter }</strong></p>
            <button className='btn' onClick={e => this.props.ADD_VALUE(1)}>Increment + 1</button>
        </div>;
    }
}