import * as React from 'react';

export class Counter extends React.Component {
    constructor() {
        super();
        this.state = { currentCount: 0 };
    }
    render() {
        return React.createElement("div", null,
            React.createElement("h1", null, "Counter"),
            React.createElement("p", null, "This is a simple example of a React component."),
            React.createElement("p", null,
                "Current count: ",
                React.createElement("strong", null, this.state.currentCount)),
            React.createElement("button", { onClick: () => { this.incrementCounter(); } }, "Increment"));
    }
    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }
}