import React, { useState } from "react";
import {
  Dialog,
  DialogTrigger,
  DialogSurface,
  DialogTitle,
  DialogBody,
  DialogActions,
  DialogContent,
  Button,
  Field,
  Input,
  Link,
} from "@fluentui/react-components";
import { EShopApi } from "../../api/EShopApi";

type LoginProps = {
  onLoginSuccess?: () => void;
};

const Login: React.FunctionComponent<LoginProps> = ({ onLoginSuccess }) => {
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [isLogin, setIsLogin] = useState(true);

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const showModal = () => {
    setModalIsOpen(true);
  };

  const hideModal = () => {
    setModalIsOpen(false);
  };

  const login = async () => {
    const api = new EShopApi();
    const response = isLogin
      ? await api.login(email, password)
      : await api.register(name, email, password);
    if (response.IsSuccess && onLoginSuccess) {
      onLoginSuccess();
      hideModal();
    }
  };

  return (
    <Dialog open={modalIsOpen}>
      <DialogTrigger disableButtonEnhancement>
        <Button onClick={showModal}>Авторизация</Button>
      </DialogTrigger>
      <DialogSurface>
        <DialogBody>
          <DialogTitle>{isLogin ? "Авторизация" : "Регистрация"}</DialogTitle>
          <DialogContent>
            {!isLogin && (
              <Field label="Имя" required>
                <Input
                  onChange={(ev, data) => {
                    setName(data.value);
                  }}
                />
              </Field>
            )}
            <Field label="Почта" required>
              <Input
                onChange={(ev, data) => {
                  setEmail(data.value);
                }}
              />
            </Field>
            <Field label="Пароль" required>
              <Input
                onChange={(ev, data) => {
                  setPassword(data.value);
                }}
              />
            </Field>
          </DialogContent>
          <Link onClick={() => setIsLogin(!isLogin)}>
            {isLogin
              ? "Еще не зарегистрированы? Регистрация"
              : "Уже зарегистрированы? Авторизация"}
          </Link>
          <DialogActions>
            <DialogTrigger disableButtonEnhancement>
              <Button appearance="secondary" onClick={hideModal}>Отмена</Button>
            </DialogTrigger>
            <Button appearance="primary" onClick={login}>
              {isLogin ? "Войти" : "Зарегистрироваться"}
            </Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  );
};

export { Login };
