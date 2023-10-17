import React, { Component } from "react";
import PlaninaDataService from "../../services/planina.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class PromjeniPlanina extends Component {

  constructor(props) {
    super(props);

    this.planina = this.dohvatiPlanina();
    this.promjeniPlanina = this.promjeniPlanina.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    
    


    this.state = {
      planina: {}
    };
  }


  async dohvatiPlanina() {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    await PlaninaDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          planina: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async promjeniPlanina(planina) {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await PlaninaDataService.put(niz[niz.length-1],planina);
    if(odgovor.ok){
      window.location.href='/planine';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    // Prevent the browser from reloading the page
    e.preventDefault();

    // Read the form data
    const podaci = new FormData(e.target);
    //Object.keys(formData).forEach(fieldName => {
    // console.log(fieldName, formData[fieldName]);
    //})
    
    //console.log(podaci.get('verificiran'));
    // You can pass formData as a service body directly:

    this.promjeniPlanina({
      ime: podaci.get('ime'),
      drzava: podaci.get('drzava'),
      visina: podaci.get('visina'),
      
    });
    
  }


  render() {
    
    const { planina} = this.state;

    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


        <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Papuk" maxLength={255} defaultValue={planina.ime} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="drzava">
            <Form.Label>Drzava</Form.Label>
            <Form.Control type="text" name="drzava" placeholder="Hrvatska" defaultValue={planina.drzava}  required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="visina">
            <Form.Label>Visina</Form.Label>
            <Form.Control type="text" name="visina" placeholder="89898" defaultValue={planina.visina}  />
          </Form.Group>

          
        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/planine`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni planinu
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

