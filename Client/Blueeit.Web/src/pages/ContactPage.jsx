import PageLayout from "../components/PageLayout";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";

export default function ContactPage() {
    return (
        <>
            <PageLayout sidebar={<><OnlineMembersPanel/></>}>
                <h1>Contact</h1>
            </PageLayout>
        </>
    );
}