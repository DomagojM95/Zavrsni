import React, { Component } from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

import logo from '../logo.svg';


export default class Izbornik extends Component{


    render(){
        return (

            <Navbar expand="lg" className="bg-body-tertiary">
            <Container>
              <Navbar.Brand href="/"> <img className="App-logo" src={logo} alt="" /> Planinarski Dnevnik</Navbar.Brand>
              <Navbar.Toggle aria-controls="basic-navbar-nav" />
              <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                  <Nav.Link href="/nadzornaploca">Nadzorna ploča</Nav.Link>
                  <NavDropdown title="Opcije" id="basic-nav-dropdown">
                    <NavDropdown.Item href="/planinar">Planinar</NavDropdown.Item>
                    <NavDropdown.Item href="/izlet">
                      izlet
                    </NavDropdown.Item>
                    <NavDropdown.Item href="/planina">planina</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="/dnevnik">
                      Dnevnik
                    </NavDropdown.Item>
                    <NavDropdown.Item target="_blank" href="http://domagojm95-001-site1.atempurl.com/swagger/index.html">
                      Swagger
                    </NavDropdown.Item>
                  </NavDropdown>
                </Nav>
              </Navbar.Collapse>
            </Container>
          </Navbar>



        );
    }
}