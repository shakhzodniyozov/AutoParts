import { Container, Nav, Navbar } from "react-bootstrap";
import { BiLogOut } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import authService from "../services/auth.service";

export function AdminNavbar() {

  const navigate = useNavigate();

  return (
    <Container>
      <Navbar collapseOnSelect bg="light" variant="light" expand="lg">
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Brand as={Link} to="/">Главная</Navbar.Brand>
        <Navbar.Collapse id="responsive-navbar-nav" className="justify-content-end">
          <Nav>
            <Nav.Link eventKey={7} as={Link} to="/sales">Продажи</Nav.Link>
            <Nav.Link eventKey={8} as={Link} to="/reports">Отчёт</Nav.Link>
            <Nav.Link eventKey={1} as={Link} to="/employees">Сотрудники</Nav.Link>
            <Nav.Link eventKey={2} as={Link} to="/delivery-of-goods">Поставки товаров</Nav.Link>
            <Nav.Link eventKey={3} as={Link} to="/categories">Категория</Nav.Link>
            <Nav.Link eventKey={4} as={Link} to="/manufactors">Производители</Nav.Link>
            <Nav.Link eventKey={5} as={Link} to="/products" className="align-items-center">Продукты</Nav.Link>
            <Nav.Link
              eventKey={6}
              as={Link}
              to=""
              onClick={() => {
                authService.signOut();
                navigate("/signin");
              }}
            >
              Выйти
              <BiLogOut size={32} />
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    </Container >
  )
}