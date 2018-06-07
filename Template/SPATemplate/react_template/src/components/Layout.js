import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import logo from './logo.svg';
import './Layout.css';

export class Layout extends React.Component {
    render() {
        return React.createElement("div", { className: 'container-fluid' },
            React.createElement("div", { className: 'App' },
                 React.createElement("header", { className: 'App-header' },
                     React.createElement("img", { className: 'App-logo', src: {logo}, alt: 'logo' }),
                     React.createElement("h1", { className: 'App-title' })),
                 React.createElement("div", { className: 'row' },
                     React.createElement("div", { className: 'col-sm-3' },
                         React.createElement(NavMenu, null)),
                     React.createElement("div", { className: 'col-sm-9' }, this.props.children))));
    }
}