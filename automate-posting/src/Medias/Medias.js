import React, { useState, useEffect } from "react";
import client, { config } from '../utils';
import ImageCropDialog from "../ImageCropDialog";
import facebook from '../Images/facebook.svg'
import instagram from '../Images/instagram.svg'
import twitter from '../Images/twitter.svg'
import { Col, Row, Container } from "react-bootstrap";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import "../Projects/Projects.css"
import 'bootstrap/dist/css/bootstrap.css';
import Alert from 'react-bootstrap/Alert';


const Medias = ({
    project
}) => {
    const [productsInProject, setProductsInProject] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState();
    const [platform, setPlatform] = useState();
    const [show, setShow] = useState(false);
    const [showw, setShoww] = useState(false);
    const handleClose = () => setShow(false);
    const handleClosee = () => setShoww(false);
    const handleShow = () => setShow(true);
    const handleShoww = () => setShoww(true);
    const folderPath = '/projects/' + project + '/medias'
    useEffect(() => {
        client.get(`DropBox/GetFolders/${encodeURIComponent(folderPath)}`, { headers: { 'Content-Type': 'application/json', } })
            .then(response => {
                setProductsInProject(response.data);
            })
            .catch(error => {
                console.error('Error fetching image links:', error);
            });
    }, []);
    const onProductSelect = (product) => {
        setSelectedProduct(product)
        handleShow()
    }
    const onPlatformSelect = (p) => {
        setPlatform(p)
    }
    return (
        <>
            <Modal show={showw} onHide={handleClosee}>
                <Modal.Body>
                    <Alert variant="success"><br></br> Image Cropping and processing has started !<br></br><br></br></Alert>
                </Modal.Body>
                <Modal.Footer >
                    <Button style={{ width: '100px' }} variant="primary" onClick={() => { handleClosee() }}>
                        Ok
                    </Button>

                </Modal.Footer>
            </Modal>

            {selectedProduct && platform && (
                <ImageCropDialog
                    selectedPlatform={platform}
                    open={true}
                    handleClose={() => setSelectedProduct(null)}
                    product={selectedProduct}
                    project={project}
                    onCropCompleteAndBeginProcess={() => {
                        handleClosee();
                        handleShoww(); 
                    }}

                />

            )}
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Choose Platform</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className="projectsContainer">
                        <Container>
                            <Row>

                                <Col><img onClick={() => { onPlatformSelect("Facebook"); handleClose() }} xs={1} style={{ width: '40px' }} src={facebook}></img></Col>
                                <Col><img onClick={() => { onPlatformSelect("Instagram"); handleClose() }} xs={1} style={{ width: '40px' }} src={instagram}></img></Col>
                                <Col><img onClick={() => { onPlatformSelect("Twitter"); handleClose() }} xs={1} style={{ width: '40px' }} src={twitter}></img></Col>
                            </Row>

                        </Container>

                    </div></Modal.Body>

            </Modal>

            <div className="projectsContainer">


                {
                    productsInProject.map((name, index) => (
                        <>
                            <div class="folder">
                                <span class="folder-icon">&#128193;</span>

                                <span onClick={() => onProductSelect(name)} class="folder-name">{name}</span>
                            </div>
                        </>
                    ))
                }

            </div>  <br></br>

        </>
    )
}
export default Medias