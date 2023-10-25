import React, { Component } from "react";
import DnevnikDataService from "../../services/dnevnik.services";
import IzletDataService from "../../services/izleti.services";
import PlaninarDataService from "../../services/planinar.services";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";
import moment from 'moment';



export default class DodajDnevnik extends Component {

  constructor(props) {
    super(props);
   
    this.dodajDnevnik = this.dodajDnevnik.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.dohvatiPlaninari = this.dohvatiPlaninari.bind(this);
    this.dohvatiIzleti = this.dohvatiIzleti.bind(this);

    this.state = {
      planinari: [],
      izleti: [],
      sifraPlaninar:0,
      sifraIzlet:0
    };
  }

  componentDidMount() {
    //console.log("Dohvaćam smjerove");
    this.dohvatiPlaninari();
    this.dohvatiIzleti();
  }

  async dodajDnevnik(dnevnik) {
    const odgovor = await DnevnikDataService.post(dnevnik);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/dnevnik';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }


  async dohvatiIzleti() {

    await IzletDataService.getAll()
      .then(response => {
        this.setState({
          izleti: response.data,
          sifraIzlet: response.data[0].sifra
        });

       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  async dohvatiPlaninari() {

    await PlaninarDataService.getAll()
      .then(response => {
        this.setState({
          planinari: response.data,
          sifraPlaninar: response.data[0].sifra
        });

       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }


  handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
   
    this.dodajDnevnik({
      naziv: podaci.get('naziv'),
      izlet: podaci.get('izlet'),
      planinar: podaci.get('planinar'),
    
      sifraIzlet: this.state.sifraIzlet,
      sifraPlaninar: this.state.sifraPlaninar
    });
    
  }


  render() { 
    const {planinari} = this.state;
    const {izleti} = this.state;
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="naziv">
            <Form.Label>Naziv</Form.Label>
            <Form.Control type="text" name="naziv" placeholder="" maxLength={255} required/>
          </Form.Group>

          <Form.Group className="mb-3" controlId="izlet">
            <Form.Label>Izlet</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraIzlet: e.target.value});
            }}>
            {izleti && izleti.map((izlet,index) => (
                  <option key={index} value={izlet.sifra}>{izlet.naziv}</option>

            ))}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3" controlId="planinar">
            <Form.Label>Planinar</Form.Label>
            <Form.Select onChange={e => {
              this.setState({ sifraPlaninar: e.target.value});
            }}>
            {planinari && planinari.map((planinar,index) => (
                  <option key={index} value={planinar.sifra}>{planinar.ime}</option>

            ))}
            </Form.Select>
          </Form.Group>

          
         



          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/dnevnik`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj dnevnik
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}
