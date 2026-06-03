-- Beauty Marketplace - Modelo relacional MySQL
-- Entrega 3 - Projeto Integrador - Bloco Arquitetura

CREATE DATABASE IF NOT EXISTS beauty_marketplace
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE beauty_marketplace;

-- Usuários e perfis de acesso
CREATE TABLE usuarios (
    id CHAR(36) PRIMARY KEY,
    nome VARCHAR(120) NOT NULL,
    email VARCHAR(180) NOT NULL UNIQUE,
    tipo ENUM('CONSUMIDOR', 'LOJISTA', 'ADMINISTRADOR') NOT NULL,
    telefone VARCHAR(20) NULL,
    senha_hash VARCHAR(255) NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE lojistas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id CHAR(36) NULL,
    nome_fantasia VARCHAR(140) NOT NULL,
    razao_social VARCHAR(180) NOT NULL,
    cnpj VARCHAR(20) NOT NULL UNIQUE,
    email VARCHAR(180) NOT NULL,
    status ENUM('PENDENTE', 'APROVADO', 'REPROVADO') NOT NULL DEFAULT 'PENDENTE',
    cidade VARCHAR(100) NOT NULL,
    estado CHAR(2) NOT NULL,
    documento_url VARCHAR(255) NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_lojistas_usuarios
        FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
) ENGINE=InnoDB;

-- Catálogo e comissões
CREATE TABLE categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(80) NOT NULL UNIQUE,
    descricao VARCHAR(255) NOT NULL,
    ativa BOOLEAN NOT NULL DEFAULT TRUE
) ENGINE=InnoDB;

CREATE TABLE comissoes_categoria (
    id INT AUTO_INCREMENT PRIMARY KEY,
    categoria_id INT NOT NULL UNIQUE,
    percentual DECIMAL(5,2) NOT NULL,
    vigente_desde DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_comissao_percentual
        CHECK (percentual >= 0 AND percentual <= 100),
    CONSTRAINT fk_comissoes_categorias
        FOREIGN KEY (categoria_id) REFERENCES categorias(id)
) ENGINE=InnoDB;

CREATE TABLE produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    slug VARCHAR(160) NOT NULL UNIQUE,
    lojista_id INT NOT NULL,
    categoria_id INT NOT NULL,
    nome VARCHAR(160) NOT NULL,
    descricao TEXT NOT NULL,
    marca VARCHAR(100) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    estoque INT NOT NULL DEFAULT 0,
    sku_lojista VARCHAR(80) NULL,
    imagem_url VARCHAR(255) NOT NULL,
    tipo_pele VARCHAR(60) NOT NULL,
    tipo_cabelo VARCHAR(60) NOT NULL,
    curvatura_cachos VARCHAR(40) NOT NULL,
    tom VARCHAR(60) NOT NULL,
    acabamento VARCHAR(60) NOT NULL,
    vegano BOOLEAN NOT NULL DEFAULT FALSE,
    composicao TEXT NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    status_moderacao VARCHAR(20) NOT NULL DEFAULT 'Aprovado',
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT chk_produtos_preco
        CHECK (preco >= 0),
    CONSTRAINT chk_produtos_estoque
        CHECK (estoque >= 0),
    CONSTRAINT fk_produtos_lojistas
        FOREIGN KEY (lojista_id) REFERENCES lojistas(id),
    CONSTRAINT fk_produtos_categorias
        FOREIGN KEY (categoria_id) REFERENCES categorias(id)
) ENGINE=InnoDB;

-- Pedidos, split e rastreio
CREATE TABLE pedidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id CHAR(36) NOT NULL,
    metodo_pagamento VARCHAR(40) NOT NULL,
    cep_entrega VARCHAR(12) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    frete DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    status VARCHAR(60) NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_pedidos_usuarios
        FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
) ENGINE=InnoDB;

CREATE TABLE pedido_itens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT NOT NULL,
    produto_id INT NOT NULL,
    lojista_id INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    percentual_comissao DECIMAL(5,2) NOT NULL,
    valor_comissao DECIMAL(10,2) NOT NULL,
    valor_repasse_lojista DECIMAL(10,2) NOT NULL,
    codigo_rastreio VARCHAR(40) NOT NULL,
    status_entrega VARCHAR(80) NOT NULL,
    CONSTRAINT chk_pedido_itens_quantidade
        CHECK (quantidade > 0),
    CONSTRAINT fk_pedido_itens_pedidos
        FOREIGN KEY (pedido_id) REFERENCES pedidos(id),
    CONSTRAINT fk_pedido_itens_produtos
        FOREIGN KEY (produto_id) REFERENCES produtos(id),
    CONSTRAINT fk_pedido_itens_lojistas
        FOREIGN KEY (lojista_id) REFERENCES lojistas(id)
) ENGINE=InnoDB;

-- Prova social e lista de desejos
CREATE TABLE avaliacoes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    produto_id INT NOT NULL,
    usuario_id CHAR(36) NOT NULL,
    nota TINYINT NOT NULL,
    comentario TEXT NOT NULL,
    midia_url VARCHAR(255) NULL,
    tipo_pele VARCHAR(60) NULL,
    tipo_cabelo VARCHAR(60) NULL,
    compra_verificada BOOLEAN NOT NULL DEFAULT FALSE,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_avaliacoes_nota
        CHECK (nota BETWEEN 1 AND 5),
    CONSTRAINT fk_avaliacoes_produtos
        FOREIGN KEY (produto_id) REFERENCES produtos(id),
    CONSTRAINT fk_avaliacoes_usuarios
        FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
) ENGINE=InnoDB;

CREATE TABLE lista_desejos_itens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id CHAR(36) NOT NULL,
    produto_id INT NOT NULL,
    criado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT uk_lista_usuario_produto
        UNIQUE (usuario_id, produto_id),
    CONSTRAINT fk_lista_usuarios
        FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    CONSTRAINT fk_lista_produtos
        FOREIGN KEY (produto_id) REFERENCES produtos(id)
) ENGINE=InnoDB;

-- Carrinho persistente para recuperação de itens do consumidor
CREATE TABLE carrinho_persistido_itens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_email VARCHAR(255) NOT NULL,
    produto_id INT NOT NULL,
    quantidade INT NOT NULL,
    atualizado_em DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expira_em DATETIME NOT NULL,
    CONSTRAINT uk_carrinho_usuario_produto
        UNIQUE (usuario_email, produto_id),
    CONSTRAINT chk_carrinho_quantidade
        CHECK (quantidade > 0),
    CONSTRAINT fk_carrinho_produtos
        FOREIGN KEY (produto_id) REFERENCES produtos(id)
) ENGINE=InnoDB;

-- Indices para consultas frequentes
CREATE INDEX idx_lojistas_status ON lojistas (status);
CREATE INDEX idx_produtos_filtros ON produtos (categoria_id, tipo_pele, tipo_cabelo, tom, vegano);
CREATE INDEX idx_produtos_lojista ON produtos (lojista_id, ativo);
CREATE INDEX idx_produtos_moderacao ON produtos (status_moderacao);
CREATE INDEX idx_pedidos_usuario ON pedidos (usuario_id, criado_em);
CREATE INDEX idx_itens_lojista ON pedido_itens (lojista_id, status_entrega);
CREATE INDEX idx_avaliacoes_produto ON avaliacoes (produto_id, criado_em);
CREATE INDEX idx_carrinho_expiracao ON carrinho_persistido_itens (expira_em);
