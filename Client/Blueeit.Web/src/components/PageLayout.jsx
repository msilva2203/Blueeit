export default function PageLayout({children, sidebar}) {
    return (
        <>
            <div className="blueeit-main-page-withsidebar">
                <div className="blueeit-main-page-content">
                    {children}
                </div>
                
                <div className="blueeit-amin-page-sidebar">
                    {sidebar}
                </div>
            </div>
        </>
    );
}