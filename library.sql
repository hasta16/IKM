--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

-- Started on 2025-01-23 04:38:44

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 222 (class 1259 OID 16459)
-- Name: authors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.authors (
    authorid integer NOT NULL,
    authorname character varying(255) NOT NULL
);


ALTER TABLE public.authors OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16458)
-- Name: authors_authorid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.authors_authorid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.authors_authorid_seq OWNER TO postgres;

--
-- TOC entry 4856 (class 0 OID 0)
-- Dependencies: 221
-- Name: authors_authorid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.authors_authorid_seq OWNED BY public.authors.authorid;


--
-- TOC entry 224 (class 1259 OID 16468)
-- Name: bookauthors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bookauthors (
    recordid integer NOT NULL,
    bookid integer NOT NULL,
    authorid integer NOT NULL
);


ALTER TABLE public.bookauthors OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16467)
-- Name: bookauthors_recordid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bookauthors_recordid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.bookauthors_recordid_seq OWNER TO postgres;

--
-- TOC entry 4857 (class 0 OID 0)
-- Dependencies: 223
-- Name: bookauthors_recordid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bookauthors_recordid_seq OWNED BY public.bookauthors.recordid;


--
-- TOC entry 220 (class 1259 OID 16446)
-- Name: books; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.books (
    bookid integer NOT NULL,
    title character varying(255) NOT NULL,
    yearpublished integer,
    genreid integer NOT NULL,
    CONSTRAINT books_yearpublished_check CHECK ((yearpublished > 0))
);


ALTER TABLE public.books OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16445)
-- Name: books_bookid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.books_bookid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.books_bookid_seq OWNER TO postgres;

--
-- TOC entry 4858 (class 0 OID 0)
-- Dependencies: 219
-- Name: books_bookid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.books_bookid_seq OWNED BY public.books.bookid;


--
-- TOC entry 218 (class 1259 OID 16437)
-- Name: genres; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.genres (
    genreid integer NOT NULL,
    genrename character varying(100) NOT NULL
);


ALTER TABLE public.genres OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16436)
-- Name: genres_genreid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.genres_genreid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.genres_genreid_seq OWNER TO postgres;

--
-- TOC entry 4859 (class 0 OID 0)
-- Dependencies: 217
-- Name: genres_genreid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.genres_genreid_seq OWNED BY public.genres.genreid;


--
-- TOC entry 228 (class 1259 OID 16494)
-- Name: loans; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.loans (
    loanid integer NOT NULL,
    bookid integer NOT NULL,
    readerid integer NOT NULL,
    loandate date NOT NULL,
    returndate date
);


ALTER TABLE public.loans OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 16493)
-- Name: loans_loanid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.loans_loanid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.loans_loanid_seq OWNER TO postgres;

--
-- TOC entry 4860 (class 0 OID 0)
-- Dependencies: 227
-- Name: loans_loanid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.loans_loanid_seq OWNED BY public.loans.loanid;


--
-- TOC entry 226 (class 1259 OID 16485)
-- Name: readers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.readers (
    readerid integer NOT NULL,
    readername character varying(255) NOT NULL,
    contactinfo character varying(255)
);


ALTER TABLE public.readers OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16484)
-- Name: readers_readerid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.readers_readerid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.readers_readerid_seq OWNER TO postgres;

--
-- TOC entry 4861 (class 0 OID 0)
-- Dependencies: 225
-- Name: readers_readerid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.readers_readerid_seq OWNED BY public.readers.readerid;


--
-- TOC entry 4668 (class 2604 OID 16462)
-- Name: authors authorid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.authors ALTER COLUMN authorid SET DEFAULT nextval('public.authors_authorid_seq'::regclass);


--
-- TOC entry 4669 (class 2604 OID 16471)
-- Name: bookauthors recordid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookauthors ALTER COLUMN recordid SET DEFAULT nextval('public.bookauthors_recordid_seq'::regclass);


--
-- TOC entry 4667 (class 2604 OID 16449)
-- Name: books bookid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books ALTER COLUMN bookid SET DEFAULT nextval('public.books_bookid_seq'::regclass);


--
-- TOC entry 4666 (class 2604 OID 16440)
-- Name: genres genreid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.genres ALTER COLUMN genreid SET DEFAULT nextval('public.genres_genreid_seq'::regclass);


--
-- TOC entry 4671 (class 2604 OID 16497)
-- Name: loans loanid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loans ALTER COLUMN loanid SET DEFAULT nextval('public.loans_loanid_seq'::regclass);


--
-- TOC entry 4670 (class 2604 OID 16488)
-- Name: readers readerid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.readers ALTER COLUMN readerid SET DEFAULT nextval('public.readers_readerid_seq'::regclass);


--
-- TOC entry 4844 (class 0 OID 16459)
-- Dependencies: 222
-- Data for Name: authors; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.authors (authorid, authorname) FROM stdin;
1	Айзек Азимов
2	Агата Кристи
3	Джейн Остин
\.


--
-- TOC entry 4846 (class 0 OID 16468)
-- Dependencies: 224
-- Data for Name: bookauthors; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bookauthors (recordid, bookid, authorid) FROM stdin;
1	1	1
2	2	2
3	3	3
\.


--
-- TOC entry 4842 (class 0 OID 16446)
-- Dependencies: 220
-- Data for Name: books; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.books (bookid, title, yearpublished, genreid) FROM stdin;
1	Основание	1951	1
2	Убийство в Восточном экспрессе	1934	2
3	Гордость и предубеждение	1813	3
4	123	123	1
\.


--
-- TOC entry 4840 (class 0 OID 16437)
-- Dependencies: 218
-- Data for Name: genres; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.genres (genreid, genrename) FROM stdin;
1	Фантастика
2	Детектив
3	Роман
\.


--
-- TOC entry 4850 (class 0 OID 16494)
-- Dependencies: 228
-- Data for Name: loans; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.loans (loanid, bookid, readerid, loandate, returndate) FROM stdin;
1	1	1	2025-01-01	\N
2	2	2	2025-01-02	\N
\.


--
-- TOC entry 4848 (class 0 OID 16485)
-- Dependencies: 226
-- Data for Name: readers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.readers (readerid, readername, contactinfo) FROM stdin;
1	Иван Иванов	ivanov@example.com
2	Анна Смирнова	smirnova@example.com
\.


--
-- TOC entry 4862 (class 0 OID 0)
-- Dependencies: 221
-- Name: authors_authorid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.authors_authorid_seq', 3, true);


--
-- TOC entry 4863 (class 0 OID 0)
-- Dependencies: 223
-- Name: bookauthors_recordid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bookauthors_recordid_seq', 3, true);


--
-- TOC entry 4864 (class 0 OID 0)
-- Dependencies: 219
-- Name: books_bookid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.books_bookid_seq', 4, true);


--
-- TOC entry 4865 (class 0 OID 0)
-- Dependencies: 217
-- Name: genres_genreid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.genres_genreid_seq', 3, true);


--
-- TOC entry 4866 (class 0 OID 0)
-- Dependencies: 227
-- Name: loans_loanid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.loans_loanid_seq', 2, true);


--
-- TOC entry 4867 (class 0 OID 0)
-- Dependencies: 225
-- Name: readers_readerid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.readers_readerid_seq', 2, true);


--
-- TOC entry 4680 (class 2606 OID 16466)
-- Name: authors authors_authorname_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.authors
    ADD CONSTRAINT authors_authorname_key UNIQUE (authorname);


--
-- TOC entry 4682 (class 2606 OID 16464)
-- Name: authors authors_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.authors
    ADD CONSTRAINT authors_pkey PRIMARY KEY (authorid);


--
-- TOC entry 4684 (class 2606 OID 16473)
-- Name: bookauthors bookauthors_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookauthors
    ADD CONSTRAINT bookauthors_pkey PRIMARY KEY (recordid);


--
-- TOC entry 4678 (class 2606 OID 16452)
-- Name: books books_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_pkey PRIMARY KEY (bookid);


--
-- TOC entry 4674 (class 2606 OID 16444)
-- Name: genres genres_genrename_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.genres
    ADD CONSTRAINT genres_genrename_key UNIQUE (genrename);


--
-- TOC entry 4676 (class 2606 OID 16442)
-- Name: genres genres_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.genres
    ADD CONSTRAINT genres_pkey PRIMARY KEY (genreid);


--
-- TOC entry 4688 (class 2606 OID 16499)
-- Name: loans loans_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loans
    ADD CONSTRAINT loans_pkey PRIMARY KEY (loanid);


--
-- TOC entry 4686 (class 2606 OID 16492)
-- Name: readers readers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.readers
    ADD CONSTRAINT readers_pkey PRIMARY KEY (readerid);


--
-- TOC entry 4690 (class 2606 OID 16479)
-- Name: bookauthors bookauthors_authorid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookauthors
    ADD CONSTRAINT bookauthors_authorid_fkey FOREIGN KEY (authorid) REFERENCES public.authors(authorid);


--
-- TOC entry 4691 (class 2606 OID 16474)
-- Name: bookauthors bookauthors_bookid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bookauthors
    ADD CONSTRAINT bookauthors_bookid_fkey FOREIGN KEY (bookid) REFERENCES public.books(bookid);


--
-- TOC entry 4689 (class 2606 OID 16453)
-- Name: books books_genreid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_genreid_fkey FOREIGN KEY (genreid) REFERENCES public.genres(genreid);


--
-- TOC entry 4692 (class 2606 OID 16500)
-- Name: loans loans_bookid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loans
    ADD CONSTRAINT loans_bookid_fkey FOREIGN KEY (bookid) REFERENCES public.books(bookid);


--
-- TOC entry 4693 (class 2606 OID 16505)
-- Name: loans loans_readerid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.loans
    ADD CONSTRAINT loans_readerid_fkey FOREIGN KEY (readerid) REFERENCES public.readers(readerid);


-- Completed on 2025-01-23 04:38:45

--
-- PostgreSQL database dump complete
--

