import "./PageLayout.css";

export default function PageLayout({children, sidebar, sidenav}) {
    return (
        <>
            <div className="blueeit-main-page-layout">
                <div className="blueeit-main-page-sidenav">
                    {sidenav}
                </div>
                <div className="blueeit-main-page-content">
                    {children}
                </div>
                <div className="blueeit-main-page-sidebar">
                    {sidebar}
                </div>
            </div>
        </>
    );
}